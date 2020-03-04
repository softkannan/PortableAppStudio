Option Strict On
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports Plutonix.UIDesign

Namespace NuControl




    ' Testbed for INameProvider
    '
    ' XooBars implements INameProvider
    ' The ZItem subcollection on XooBar item uses Automatic (collection editor names it)
    '
    '
    ' Structure:
    ' NuControl has a collection property named XooBars
    '    XooBars contains XooBar items
    '        A XooBar item can also contain a (sub) collection of ZItems

    ' Using NameProvider:
    ' XooBars Collection is the FIRST call for naming XooBar items
    '    NuControl is called only if XooBars does not implement INameProvider.
    '
    ' The Zitems editor can be reset to use INameProvider - the code is all there.
    ' Using INP, the ZColBaseINP collection is the FIRST call for naming Zitems
    '    XooBar ITEM is the second call if the interface does not exist.
    '    (XooBar is the type exposing the collection property)


    ' XooBar ITEM - this is a concrete class - no inheritance
    <Serializable, TypeConverter(GetType(XooConverter))>
    Public Class XooBar
        Implements INameProvider
        ' this class Implements INameProvider in order
        ' to provide the naming service for the ZITEMS SUB COLLECTION
        ' it has no role in naming new XooBar items (itself) - that role is performed by
        ' XooBars COLLECTION (the "parent").  

        ' It is confusing with abstract demo classes: The Collection class 
        ' is called first to name an item in it.


        ' For newitems, make the name look like IComponent: (Name),  and
        ' readonly because the user cant mess with it 
        ' since we serialize NAME via the TypeConverter it is hidden
        ' the only time the name can be set it when it is created:

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        <ParenthesizePropertyName(True), [ReadOnly](True)>
        Public Property Name As String

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property Index As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property UserText As String

        ' another SUB collection of Ziggy Zoey, Zacky items.  defined below
        Private _zCol As New ZColBaseINP

        ' the Editor starts as AUTOMATIC NAMING...see ZSubCollectionEditorINP
        <Editor(GetType(ZSubCollectionEditorINP), GetType(System.Drawing.Design.UITypeEditor))>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
        Public ReadOnly Property ZSubCollection As ZColBaseINP
            Get
                Return _zCol
            End Get
        End Property

        Private Sub ResetZSubCollection()
            _zCol.Clear()
        End Sub

        Private Function ShouldSerializeZSubCollection() As Boolean
            Return (_zCol.Count > 0)
        End Function

        ' serializer/Typeconverter version
        Public Sub New(aName As String)
            MyClass.New()
            _Name = aName
        End Sub

        ' collection editor requires simple ctor.  HOWEVER,
        ' as soon as it is created, the naming service will be invoked to 
        ' provide a name
        Public Sub New()
            UserText = "New User Text"
            Index = 0
            _Name = "new"
        End Sub

        ' Provides the naming service for the CHILD collection ZSubCollection
        ' this is not called unless you change the NamingService property 
        ' in ZSubCollectionEditorINP
        Public Function GetNewName(typename As String) As String Implements INameProvider.GetNewName

            ' do whatever you want
            ' Ziggy1, Zoey1, Ziggy2, Zacky1, Zoey2    <===
            ' Ziggy1, Zoey2, Ziggy3, Zacky4, Zoey5
            ' ZItem1, ZItem2, ZItem3, ZItem4, ZItem5

            Dim baseName As String = RunTimeUIEdTools.BaseNameFromTypeName(typename)
            Return baseName & CountOfTypeName(baseName).ToString

        End Function

        ' each Ziggy, Zoey, Zacky numbered based on count of that name
        Private Function CountOfTypeName(typeName As String) As Integer
            Dim n As Integer = 1
            For Each z As ZItem In _zCol
                If z.Name.ToLowerInvariant.StartsWith(typeName.ToLowerInvariant) Then
                    n += 1
                End If
            Next
            Return n
        End Function

        ' another method
        ' append a total col count to the base typename 
        Private Function _GetNewName(aName As String, ndx As Integer) As String
            For Each z As ZItem In _zCol
                If z.Name = aName & ndx.ToString Then
                    Return _GetNewName(aName, ndx + 1)
                End If
            Next
            ' the default
            Return aName & ndx.ToString
        End Function


    End Class

    ' XooBar Item Collection
    <Serializable>
    Public Class XooBarCollection
        Inherits Collection(Of XooBar)
        Implements INameProvider

        ' provides the names for new XooBar Items

        Public Overloads Function Add(x As XooBar) As Integer
            ' extra protection when added in code:
            If ContainsName(x.Name) Then
                Throw New ApplicationException(String.Format("Name already in use: [{0}]", x.Name))
                Exit Function
            End If

            Items.Add(x)
            Return Items.Count
        End Function

        Public Function AddRange(ParamArray xoos() As XooBar) As Integer
            For Each x As XooBar In xoos
                Add(x)
            Next
            Return Items.Count
        End Function

        Default Public Overloads Property Item(ndx As Integer) As XooBar
            Get
                If (ndx <= Items.Count - 1) Then
                    Return CType(MyBase.Items(ndx), XooBar)
                Else
                    Return Nothing
                End If
            End Get
            Set(value As XooBar)
                If (ndx <= Items.Count - 1) Then
                    MyBase.Items(ndx) = value
                End If
            End Set
        End Property

        Private Function ContainsName(name As String) As Boolean
            For Each xoo As XooBar In Items
                If xoo.Name.ToLowerInvariant = name.ToLowerInvariant Then
                    Return True
                End If
            Next
            Return False
        End Function

        ' The naming service for a new XooBar Items
        Public Function GetNewName(typeName As String) As String Implements INameProvider.GetNewName
            '  normal way might be to use "XooBarNN":
            Dim basename As String = RunTimeUIEdTools.BaseNameFromTypeName(typeName)

            ' using a custom base name to show it is working thru this method
            ' if you switch to AUTO with stuff in the collection
            ' the names will be MIXED and auto names may appear to be off numerically
            basename = "NewXoo"

            Return _GetNewName(basename, Items.Count + 1)
        End Function

        Private Function _GetNewName(aName As String, ndx As Integer) As String
            For Each xoo As XooBar In Items
                If xoo.Name = aName & ndx.ToString Then
                    Return _GetNewName(aName, ndx + 1)
                End If
            Next
            ' the default
            Return aName & ndx.ToString
        End Function


    End Class


    ' another collection of Zitems - CollectionBase
    ' this is the SUBcollection on XooBar
    ' the related editor/ 
    <Serializable>
    Public Class ZColBaseINP
        Inherits CollectionBase
        Implements INameProvider

        ' as the owner of the sub collection, this class can provide the names
        ' for the sub collection.  The editor has this class
        ' set as Automatic.  If the collection editor [ZSubCollectionEditorINP] is changed
        ' to NameProvider the code should call GetNewName below for NewItem names

        Public Sub New()

        End Sub

        ' required accessors
        Public Function Add(z As ZItem) As Integer

            z.Index = MyBase.List.Count
            List.Add(z)
            Return List.Count

        End Function

        Public Function AddRange(ParamArray zzz() As ZItem) As Integer
            For Each z As ZItem In zzz
                Add(z)
            Next
            Return List.Count
        End Function

        Public Property Item(ndx As Integer) As ZItem
            Get
                If MyBase.List.Count < ndx Then
                    Return CType(MyBase.List(ndx), ZItem)
                End If
                Return Nothing
            End Get
            Set(value As ZItem)
                If MyBase.List.Count < ndx Then
                    MyBase.List(ndx) = value
                End If

            End Set
        End Property

        ' Provides the naming service for the ZSubCollection
        ' this is not called unless you change the NamingService property 
        ' in ZSubCollectionEditorINP
        Public Function GetNewName(typename As String) As String Implements INameProvider.GetNewName

            ' do whatever you want
            ' Ziggy1, Zoey1, Ziggy2, Zacky1, Zoey2
            ' Ziggy1, Zoey2, Ziggy3, Zacky4, Zoey5            <====
            ' ZItem1, ZItem2, ZItem3, ZItem4, ZItem5

            Dim baseName As String = RunTimeUIEdTools.BaseNameFromTypeName(typename)
            Return _GetNewName(baseName, MyBase.List.Count + 1)

        End Function

        Private Function _GetNewName(aName As String, ndx As Integer) As String
            For Each z As ZItem In MyBase.List
                If z.Name = aName & ndx.ToString Then
                    Return _GetNewName(aName, ndx + 1)
                End If
            Next
            ' the default
            Return aName & ndx.ToString
        End Function

    End Class


    Friend Class XooConverter
        Inherits TypeConverter

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destType As Type) As Boolean

            If destType = GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function


        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, info As Globalization.CultureInfo,
                                            value As Object, destType As Type) As Object

            If destType = GetType(InstanceDescriptor) Then

                Dim xoo As XooBar = CType(value, XooBar)

                Return New InstanceDescriptor(value.GetType. _
                                              GetConstructor(New Type() {GetType(String)}),
                                              New Object() {xoo.Name}, False)

            End If
            Return MyBase.ConvertTo(context, info, value, destType)
        End Function


    End Class

End Namespace


