Option Strict On
Imports System.Collections.ObjectModel
Imports System.ComponentModel.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization

Namespace NuControl


    ' the basic data type 
    Public Enum ItemTypes
        TextType
        ValueType
        FooType
    End Enum

    ' Structure:
    '    ExtendedItem is an abstract base class

    '  when the base class is marked as Serializable, each child class
    ' need not be. Attributes are not generally inherited, but this one
    ' marks a TYPE as serializable, so a Textitem is serializable because 
    ' it also of type ExtendedItem

    <Serializable>
    <TypeConverter(GetType(XTDItemConverter))>
    Public MustInherit Class ExtendedItem

        ' various fake properties
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        <Description("User defined name")>
        Public Property Name As String

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Fake value property")>
        Public Property PropVal As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("A string type property")>
        Public Property PropText As String

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), [ReadOnly](True)>
        <Description("Item Base type")>
        Public Property ItemType As ItemTypes

        ' we control this one
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        <Description("Item Index "), [ReadOnly](True)>
        Public Property Index As Integer

        Public Sub New()
            PropVal = 0
            PropText = ""
        End Sub

        ' all the child classes must have a type:
        Public Sub New(st As ItemTypes)
            MyClass.New()
            ItemType = st
        End Sub


    End Class

    ' a version specifically for Text

    '<Serializable, TypeConverter(GetType(XTDItemConverter))>
    Public Class TextItem
        Inherits ExtendedItem

        Private Const myType As ItemTypes = ItemTypes.TextType

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property Text As String

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property ExtendedText As String

        Public Sub New(myName As String)
            MyClass.New()
            MyBase.Name = myName
        End Sub

        Public Sub New()
            MyBase.New(myType)
            MyBase.Name = "TextItem"
            Text = ""
            ExtendedText = ""
        End Sub

    End Class

    ' VTypes have a classification; the EnumConverter provides better names
    <TypeConverter(GetType(ValueEnumConverter))>
    Public Enum ValueTypes
        VInteger
        VLong
        VFloat
        VDecimal
    End Enum

    ' a version specifically for values  
    '<Serializable>
    '<TypeConverter(GetType(XTDItemConverter))>
    Public Class ValueItem
        Inherits ExtendedItem

        Private Const myType As ItemTypes = ItemTypes.ValueType

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Current Item value")>
        Public Property Value As Decimal

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Value type")>
        Public Property ValueType As ValueTypes

        Public Sub New(myName As String)
            MyClass.New()
            MyBase.Name = myName
        End Sub

        Public Sub New()
            MyBase.New(myType)
            MyBase.Name = "ValueItem"
            Value = 42
            ValueType = ValueTypes.VInteger
        End Sub

    End Class


    ' Foolean version 
    '<Serializable> <TypeConverter(GetType(XTDItemConverter))>
    '<TypeConverter(GetType(XTDItemConverter))>
    Public Class FooBarItem
        Inherits ExtendedItem

        Private Const myType As ItemTypes = ItemTypes.FooType

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("True/False: is Elvis Alive")>
        Public Property SomeBoolean As Boolean

        Public Sub New()
            MyBase.New(myType)
            Name = "FooItem"
            SomeBoolean = False
        End Sub

        Public Sub New(myName As String)
            MyClass.New()
            MyBase.Name = myName
        End Sub

        ' these are called from the editor to create a new name
        ' Foos and Bars do not have a collection class
        ' there are a quick and dirty List<T>
        ' dont try this at home
        Friend Function NewFooItem(item As Object) As Object
            DirectCast(item, Foo).FooValue = _colFoos.Count
            DirectCast(item, Foo).Name = "thisNewFoo" & (_colFoos.Count + 1).ToString
            Return item
        End Function

        ' simple proc to make a name based on col count (which is "one behind" the editor!)
        Friend Function GetNewBarName() As String
            Return "NewBar" & (_colBars.Count + 1).ToString
        End Function

        ' sub collection 1 = Foo

        ' done in the interest of expediency
        Private _colFoos As New List(Of Foo)

        <Description("Simple sub Collection of Foo")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(FooBarCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property Foos As List(Of Foo)
            Get
                Return _colFoos
            End Get
        End Property

        Private Sub ResetFoos()
            _colFoos.Clear()
        End Sub

        Private Function ShouldSerializeFoos() As Boolean
            Return (_colFoos.Count > 0) ' IsNot Nothing)
        End Function

        ' sub collection 2 = Bar

        ' done in the interest of expediency
        Private _colBars As New List(Of Bar)

        <Description("Simple nested Collection of Bars")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(FooBarCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property Bars As List(Of Bar)
            Get
                Return _colBars
            End Get
        End Property

        Private Sub ResetBars()
            _colBars.Clear()
        End Sub

        Private Function ShouldSerializeBars() As Boolean
            Return (_colBars.Count > 0) ' IsNot Nothing)
        End Function


        ' sub Collection 3 = ZITems 
        ' the UIEditor will exclude ZIGGY as an illegal 
        ' member for usage in the sub collection. 

        Private _colZ As New ZItems            ' ZItems is a Collection<T> in ZItems

        <Description("Collection of Ziggy-less ZItems")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(ZItemSubCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public Property ZSubCollection As ZItems
            Get
                Return _colZ
            End Get
            Set(value As ZItems)

            End Set
        End Property

        Private Sub ResetZSubCollection()
            _colZ.Clear()
        End Sub

        Private Function ShouldSerializeZSubCollection() As Boolean
            Return (_colZ.Count > 0)
        End Function
    End Class

    ' Foo and Bar use the same typeconverter
    <Serializable, TypeConverter(GetType(FooBarConverter))>
    Public Class Foo
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property Name As String

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property Index As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Foo Value")>
        Public Property FooValue As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Item Color")>
        Public Property ItemColor As Color

        Public Sub New()
            Name = "Foo"
            Index = 0
            ItemColor = Color.Cornsilk
            FooValue = -1
        End Sub

        'Public Sub New(newName As String)
        '    Name = newName
        '    Index = -1
        '    FooColor = Color.Cornsilk
        'End Sub


    End Class

    <Serializable, TypeConverter(GetType(FooBarConverter))>
    Public Class Bar
        Inherits Foo

        ' no Foos allowed...hidden == None
        ' testing simple inheritance 
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        <Browsable(False)>
        Public Shadows Property FooValue As Integer

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        <Description("Bar value")>
        Public Property BarValue As Integer

        <Description("Bar Color")>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property BarColor As Color

        <Description("Bar Text")>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
        Public Property BarString As String

        Public Sub New()
            MyBase.New()                  ' set item color
            Name = "Bar"
            Index = 0
            BarColor = Color.AliceBlue
            BarString = "This is a Bar"
        End Sub

    End Class


    ' this is the collection class which the primary class will use
    ' and expose to the collection editor
    Public Class ExtendedItems
        Inherits Collection(Of ExtendedItem)

        Sub New()

        End Sub

        ' required accessors
        Public Overloads Function Add(ByVal xtd As ExtendedItem) As Integer

            ' set the index when loading items
            ' the designer sends these to us in the order 
            ' proscribed in the collection editor
            xtd.Index = Items.Count
            Items.Add(xtd)
            Return Items.Count

        End Function

        Public Function AddRange(ParamArray xtds() As ExtendedItem) As Integer
            For Each xtd As ExtendedItem In xtds
                Add(xtd)
            Next
            Return Items.Count
        End Function


        Public Shadows Property Item(index As Integer) As ExtendedItem
            Get
                If (index <= MyBase.Items.Count - 1) Then
                    Return MyBase.Items(index)
                Else
                    Return Nothing
                End If
            End Get
            Set(value As ExtendedItem)
                MyBase.Items(index) = value
            End Set
        End Property

        ' prevent a clear or qualify it
        Public Shadows Sub Clear()

        End Sub

        ' provide local access only, where we might really do the clearing
        Friend Sub ClearAll()

        End Sub


        ' can be called from the main control/class when it is initialized
        ' add code here to perform actions after everything has been
        ' loaded from the designer
        Friend Sub EndInit()

        End Sub


    End Class


    Friend Class XTDItemConverter
        Inherits TypeConverter

        ' the XTD items all use a name param ctor
        ' as such each can use the same TConverter
        ' compare to ZigConverter and ZoeConverter
        ' the actual (concrete) Type is not referenced in ConvertTo

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destType As Type) As Boolean

            If destType = GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function


        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, info As CultureInfo, value As Object, destType As Type) As Object

            If destType = GetType(InstanceDescriptor) Then

                ' uses a string ctor
                ' these can be shared across identical types using value.GetType 
                ' cast to ExtendedItem to get the name value - 
                ' works because Name is on ExtendedItem base class
                ' and because all the concrete types are also of type ExtendedItem

                Return New InstanceDescriptor(value.GetType. _
                                              GetConstructor(New Type() {GetType(String)}),
                                              New Object() {CType(value, ExtendedItem).Name}, False)

            End If
            Return MyBase.ConvertTo(context, info, value, destType)
        End Function


    End Class

    Friend Class FooBarConverter
        Inherits TypeConverter

        ' with param less ctors
        ' the actual Type is not referenced here

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destType As Type) As Boolean

            If destType = GetType(InstanceDescriptor) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destType)
        End Function


        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, info As CultureInfo, value As Object, destType As Type) As Object

            If destType = GetType(InstanceDescriptor) Then
                ' uses a simple ctor
                ' these can be shared across identical types using value.GetType 

                Return New InstanceDescriptor(value.GetType. _
                                              GetConstructor(New Type() {}),
                                              New Object() {}, False)

            End If
            Return MyBase.ConvertTo(context, info, value, destType)
        End Function


    End Class

    ' converts the ValueTypes Enum with nicer names
    ' for the property dropdown
    Public Class ValueEnumConverter
        Inherits EnumConverter

        ' cache the translations 
        Private myText As New Dictionary(Of ValueTypes, String)

        Public Sub New()
            MyBase.New(GetType(ValueTypes))

            ' since we will have to also convert BACK to Enum value,
            ' store as a conversion table
            With myText
                .Add(ValueTypes.VInteger, "An Integer Value")
                .Add(ValueTypes.VLong, "A Long Integer Value")
                .Add(ValueTypes.VFloat, "A Fractional Value")
                .Add(ValueTypes.VDecimal, "A Large Fractional Value")
            End With

        End Sub

        Public Overrides Function CanConvertTo(context As ITypeDescriptorContext, destinationType As Type) As Boolean
            If destinationType = GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertTo(context, destinationType)
        End Function

        Public Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean
            If sourceType = GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertFrom(context, sourceType)
        End Function

        ' supplies the string translation of the enum value
        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, culture As CultureInfo,
                                            value As Object, destinationType As Type) As Object

            If destinationType = GetType(String) AndAlso TypeOf value Is ValueTypes Then
                ' return the string version of the value (enum) passed
                Return myText(CType(value, ValueTypes))

            End If


            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End Function

        ' this time, we also need a convert from.  After they pick a dropdown, the
        ' editor will need to ConvertFrom string back to the Enum value
        Public Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As CultureInfo,
                                              value As Object) As Object

            ' find the string they picked in the dropdown
            ' and return the key which is the Enum value
            For Each kvp As KeyValuePair(Of ValueTypes, String) In myText
                If value.ToString = kvp.Value Then
                    Return kvp.Key
                    Exit Function
                End If
            Next

            Return MyBase.ConvertFrom(context, culture, value)
        End Function

    End Class

End Namespace

