Option Strict On
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization
Imports System.Collections.ObjectModel

Namespace NuControl



    Public MustInherit Class ZItem

        ' various fake properties...this one is in the constructor
        Private _Name As String
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(value As String)
                _Name = value
            End Set
        End Property

        ' this is internal to categorize the item
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property ItemType As ItemTypes

        ' ...this property is normal
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property PropVal As Integer

        ' this one is managed by the class collection
        ' this can be visible and we just ignore it and set it as we like
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Item Index"), [ReadOnly](True)>
        Public Property Index As Integer

        ' all inherited classes must have a type:
        Public Sub New(st As ItemTypes, newName As String)
            ItemType = st
            Name = newName
        End Sub

    End Class

    ' each ZItem must use its own special TypeConverter
    ' because they all need a different ctor
    <Serializable>
    <TypeConverter(GetType(ZigConverter))>
    Public Class Ziggy
        Inherits ZItem

        Private Const myType As ItemTypes = ItemTypes.FooType

        ' props peculiar to Ziggy, ctor
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property ZText As String

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property ZIndex As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property ZFoo As String

        ' Type converter's constructor
        Public Sub New(NewName As String, ndx As Integer, NameTxt As String)
            MyBase.New(myType, NewName)
            ZText = NameTxt
            ZIndex = ndx
            ZFoo = "Ziggy's Foo"
        End Sub

        ' Collection editor's constructor
        Public Sub New()
            MyClass.New("NewZiggy", -1, "ZText")
        End Sub


    End Class

    <Serializable, TypeConverter(GetType(ZoeConverter))>
    Public Class Zoey
        Inherits ZItem

        Private Const myType As ItemTypes = ItemTypes.FooType

        ' props peculiar to Zoey
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property ZCount As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property ZBar As String


        Public Sub New(newName As String, c As Integer)
            MyBase.New(myType, newName)
            MyBase.Name = newName
            ZCount = c
            ZBar = "Zoey Bar"
        End Sub

        ' Collection editor's constructor
        Public Sub New()
            MyClass.New("NewZoey", 0)

        End Sub

    End Class

    <Serializable>
    <TypeConverter(GetType(ZackConverter))>
    Public Class Zacky
        Inherits ZItem

        Private Const myType As ItemTypes = ItemTypes.FooType

        ' props peculiar to Zack
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public Property ZIndex As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property ZFoo As String

        ' Type converter's constructor
        Public Sub New(newName As String)
            MyClass.New()
            MyBase.Name = newName
        End Sub

        ' for collection editor
        Public Sub New()
            MyBase.New(myType, "NewZacky")
            ZIndex = 0
            ZFoo = "Zack's Foo"
        End Sub


    End Class

    ' Zitems are used repeatedly as a sub collection
    ' and a few top level collections
    ' 
    ' collection classes built on several collection types are used.
    ' this collection class - Collection<T>
    Public Class ZItems
        Inherits Collection(Of ZItem)

        ' provides a Items container
        '  and an Add and Item accessor in the base class

        Sub New()

        End Sub

        ' required accessors
        Public Overloads Function Add(ByVal zit As ZItem) As Integer

            ' set the index when loading items
            ' the designer sends these to us in the order 
            ' proscribed in the collection editor

            zit.Index = Items.Count
            Items.Add(zit)
            Return Items.Count

        End Function

        Public Function AddRange(ParamArray zits() As ZItem) As Integer
            For Each z As ZItem In zits
                Add(z)
            Next
            Return Items.Count
        End Function

        Public Overloads Property Item(ndx As Integer) As ZItem
            Get
                If (ndx <= Items.Count - 1) Then
                    Return CType(MyBase.Items(ndx), ZItem)
                Else
                    Return Nothing
                End If
            End Get
            Set(value As ZItem)
                If (ndx <= Items.Count - 1) Then
                    MyBase.Items(ndx) = value
                End If
            End Set
        End Property


    End Class

    ' another coll class - CollectionBase
    Public Class ZColBase
        Inherits CollectionBase

        ' CollectionBase provides the innerlist
        '   container as List or InnerList
        ' It does NOT provide the required ADD or ITEM members
        ' But it DOES provide some cool methods like
        '    Onremove, OnInsert 


        Public Sub New()

        End Sub

        Public Function Add(z As ZItem) As Integer

            z.Index = List.Count
            List.Add(z)
            Return List.Count

        End Function

        Public Function AddRange(ParamArray Zs() As ZItem) As Integer
            For Each z As ZItem In Zs
                Add(z)
            Next
            Return List.Count
        End Function

        ' swap the Property and Function versions to get a scolding
        ' from the new collection editor
        'Public Function Item(ndx As Integer) As ZItem
        '    If List.Count < ndx Then
        '        Console.Beep()
        '        Return CType(List(ndx), ZItem)
        '    End If
        '    Return Nothing
        'End Function

        Default Public Property Item(ndx As Integer) As ZItem
            Get
                If List.Count < ndx Then
                    Return CType(List(ndx), ZItem)
                End If
                Return Nothing
            End Get
            Set(value As ZItem)
                If List.Count < ndx Then
                    List(ndx) = value
                End If
            End Set
        End Property



    End Class

    Public Class ZObservable
        Inherits ObservableCollection(Of ZItem)

        Public Overloads Sub Add(z As ZItem)
            If Items.Contains(z) = False Then
                Items.Add(z)
            End If
        End Sub

        Public Sub AddRange(ParamArray zEls() As ZItem)
            For Each z As ZItem In zEls
                Add(z)
            Next
        End Sub

        Public Overloads Property Item(index As Integer) As ZItem
            Get
                If index < Items.Count Then
                    Return Items.Item(index)
                Else
                    Return Nothing
                End If

            End Get
            Set(value As ZItem)
                If Items.Count < index Then
                    Items(index) = value
                End If
            End Set
        End Property

        ' Friend - external enemies cannot invade
        Friend Overloads Sub Clear()
            Items.Clear()
        End Sub


    End Class

    Friend Class ZigConverter
        Inherits TypeConverter

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destType As Type) As Boolean

            If destType = GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function


        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, info As CultureInfo,
                                            value As Object, destType As Type) As Object


            If destType = GetType(InstanceDescriptor) Then
                Dim z As Ziggy = CType(value, Ziggy)
                ' ziggy's ctor is string, int , string
                ' tell NET it looks like that using GetType
                ' then pass the values as an object array
                ' false means this is not all there is
                Return New InstanceDescriptor(GetType(Ziggy). _
                                              GetConstructor(New Type() {GetType(String),
                                                                         GetType(Integer),
                                                                         GetType(String)}),
                                              New Object() {z.Name, z.ZIndex, z.ZText}, False)

            End If
            Return MyBase.ConvertTo(context, info, value, destType)
        End Function


    End Class

    Friend Class ZoeConverter
        Inherits TypeConverter

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext,
                                               destType As Type) As Boolean
            If destType = GetType(InstanceDescriptor) Then
                ' Yes I Can
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function

        Public Overrides Function ConvertTo(context As ITypeDescriptorContext,
                                            info As CultureInfo, value As Object,
                                            destType As Type) As Object

            If destType = GetType(InstanceDescriptor) Then
                Dim z As Zoey = CType(value, Zoey)

                ' prepare a constructor info
                Dim ctor As Reflection.ConstructorInfo

                ' the ctor wanted, is the one for a Zoey, which takes a string, and an Integer
                ctor = GetType(Zoey).GetConstructor(New Type() {GetType(String), GetType(Integer)})
                ' return Instance Descriptor built from ctor info and an array of the current
                '   values for the ctor params
                Return New InstanceDescriptor(ctor,
                            New Object() {z.Name, z.ZCount}, False)

            End If
            Return MyBase.ConvertTo(context, info, value, destType)


        End Function

    End Class

    Friend Class ZackConverter
        Inherits TypeConverter

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destType As Type) As Boolean

            If destType = GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function


        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, info As CultureInfo, value As Object, destType As Type) As Object

            If destType = GetType(InstanceDescriptor) Then

                ' just name is needed to make a new Zacky
                Dim z As Zacky = CType(value, Zacky)

                Return New InstanceDescriptor(value.GetType. _
                                              GetConstructor(New Type() {GetType(String)}),
                                              New Object() {z.Name}, False)

            End If
            Return MyBase.ConvertTo(context, info, value, destType)
        End Function


    End Class

End Namespace