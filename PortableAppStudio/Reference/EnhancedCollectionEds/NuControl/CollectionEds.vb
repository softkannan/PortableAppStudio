Option Strict On
Imports System.Reflection
Imports System.Windows.Forms
Imports System.ComponentModel.Design               ' in system.design.dll
Imports Plutonix.UIDesign
Imports System.ComponentModel.Design.Serialization
Imports System.ComponentModel
Imports NuControl

'  *****
'  All the "little" collection editors used in the demo
'  are defined here.
'  *****



' collection editor for the XTDR items 
' uses Collection of T
Public Class XTDItemCollectionEditor
    Inherits EnhancedCollectionEditor


    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "Extended Item Collection Editor"
        MyBase.ShowPropGridHelp = True
        MyBase.AllowMultipleSelect = False

        MyBase.UsePropGridChangeEvent = True
        AddHandler MyBase.PropertyValueChanged, AddressOf mypropG_PropertyValueChanged

    End Sub

    Private Sub mypropG_PropertyValueChanged(sender As Object,
                                             e As PropertyValueChangedEventArgs)
        ' your code here

    End Sub


    ' How to Get a Reference to a Control on the Editor Form

    ' in most cases, the PropertyValueChanged event should allow you to evaluate
    ' and do whatever you want. For more extraordinary situations, you
    ' can add an event handlers to editor form controls as a last resort.

    ' 1) The base class will fire the EditorFormCreated event once the form is created
    '       You cannot get to the controls elsewhere like the constructor, as they do not
    '       exist yet
    ' 2) The event will provide a form reference, use GetControlByName to get a control reference
    ' 3) the base class provides the form names as constants, IntelliSense can help: CTRL_<controlname>
    ' 4) Test that the return IsNot Nothing, then Attach handlers
    ' 5) DO NOT create a form reference.
    '       a) you have no way to release it
    '       b) it may be that this will cause other problems when a collection item
    '           itself hosts sub collections.
    '       c) DO NOT create/save/cache a form reference.  Ever.

    Private pGrid As PropertyGrid
    Private Sub XTDItemCollectionEditor_EditorFormCreated(sender As Object, efc As EditorCreatedEventArgs) Handles Me.EditorFormCreated

        pGrid = CType(MyBase.GetControlByName(CTRL_propertyBrowser, efc.EditorForm.Controls), PropertyGrid)

        If pGrid Is Nothing Then
            DisplayError(String.Format("Could not get [{0}]", CTRL_propertyBrowser))

        Else
            ' hook up your own handlers
        End If

    End Sub


End Class


' used when the ZIitems are a sub
' collection on XItem.Foo
' No Ziggys allowed
Public Class ZItemSubCollectionEditor
    Inherits EnhancedCollectionEditor

    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "Ziggy-less ZItem Collection Editor"
        MyBase.ShowPropGridHelp = True
        MyBase.AllowMultipleSelect = False
        ' pretend Ziggy is a special class which cannot
        ' be a part of the nested version
        MyBase.ExcludedTypes.Add(GetType(Ziggy))
        'MyBase.ExcludedTypes.Add(GetType(Zoey))

    End Sub

End Class

' used for the rest of the ZItem collections:
' ZItemCollection using Collection of T
' ZList where a List(of T) is used.
' ZCollectionBase using CollectionBase
Public Class ZItemCollectionEditor
    Inherits EnhancedCollectionEditor

    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "General ZItem Collection Editor"
        MyBase.ShowPropGridHelp = False
        MyBase.AllowMultipleSelect = False
        MyBase.NameService = NameServices.Automatic

    End Sub

 

End Class

' and extra editor
Public Class ZExtraCollectionEditor
    Inherits EnhancedCollectionEditor

    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "Extra ZItem Collection Editor"
        MyBase.ShowPropGridHelp = True
        MyBase.AllowMultipleSelect = False
        MyBase.NameService = NameServices.None

    End Sub

End Class


' used for the FoobarItem on XTDRItems property
' both Foos and Bars are List<T> and could just use the plain CollectionEditor
' I want a snazzy form title though
Public Class FooBarCollectionEditor
    Inherits EnhancedCollectionEditor

    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "Foos+Bars Collection Editor"
        MyBase.ShowPropGridHelp = False
        MyBase.AllowMultipleSelect = True
        MyBase.NameService = NameServices.None

    End Sub

End Class

' used for the XooBarItem on XTDRItems property
' both Foos and Bars are List<T> and could just use the plain CollectionEditor
' I want a snazzy form title though
Public Class XooBarCollectionEditor
    Inherits EnhancedCollectionEditor

    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "XooBars Collection Editor"
        MyBase.ShowPropGridHelp = False
        MyBase.AllowMultipleSelect = True

        MyBase.NameService = NameServices.NameProvider

    End Sub
End Class

' used for the ZItem sub collection
' on ExtendedItems.FooBarItem
Public Class ZSubCollectionEditorINP
    Inherits EnhancedCollectionEditor


    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "Uniquely Named ZItems Collection Editor"
        MyBase.ShowPropGridHelp = False
        MyBase.AllowMultipleSelect = True

        ' change this to 'NameProvider' to use the GetNewName
        ' methods already present
        MyBase.NameService = NameServices.Automatic

    End Sub

End Class

' include 'VBCollection.vb' in the project to tinker with this
' it doesnt work: the VB Collection cannot be inherited,  
' is not strongly typed and cannot be made to seem so, and
' it returns Object...  it is horrible
Public Class VBCollectionEditor
    Inherits EnhancedCollectionEditor


    Public Sub New(t As Type)
        MyBase.New(t)

        MyBase.FormCaption = "VB ZItems Collection Editor"
        MyBase.ShowPropGridHelp = False
        MyBase.AllowMultipleSelect = True

        MyBase.NameService = NameServices.None

    End Sub

End Class
