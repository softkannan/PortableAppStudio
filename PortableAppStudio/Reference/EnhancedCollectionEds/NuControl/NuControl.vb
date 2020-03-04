Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Reflection
Imports System.Drawing.Design
Imports Plutonix.UIDesign
Imports System.Collections.ObjectModel



' this adds some complexity to the test...
' not all properties we might want to pass to
' a collection editor might be located in the main class
' in this case, the collection is a helper class we
' will expose

Namespace NuControl



    Partial Public Class NuControl
        Inherits Panel
        Implements INameProvider                    ' second chance provider
        Implements ISupportInitialize


        ' this is the collection of Extended items we wish to 
        ' edit in the CollectionEditor...it must be instanced!
        Private XTDItems As New ExtendedItems

        ' fake vars
        Private _Intitialized As Boolean = False

        Public Sub New()
            MyBase.BorderStyle = Windows.Forms.BorderStyle.FixedSingle

            
        End Sub

        ' NET will call this before setting any of your properites
        Public Sub BeginInit() Implements ISupportInitialize.BeginInit

        End Sub

        ' NET will call this AFTER setting ALL  of your properites
        Public Sub EndInit() Implements ISupportInitialize.EndInit
            MyBase.AllowDrop = False

            ' you can extend this to other classes
            ' if there is something you need to do when all 
            ' the properties and items have been loaded
            XTDItems.EndInit()

            _Intitialized = True
        End Sub


#Region " ***  the XTD Property "

        ' Uses ExtendedItems from XItems
        ' Collection(Of T)
        ' XTDItemCollectionEditor

        <Description("Collection of Item Extenders Definitions")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(XTDItemCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property XTDRItems As ExtendedItems
            Get
                ' MS does something like this for the
                ' ListView Columns collection:
                If XTDItems Is Nothing Then
                    XTDItems = New ExtendedItems
                End If

                Return XTDItems
            End Get

        End Property

        ' these are pretty much required - they tell NET
        ' when to serialize and how to clear the collection
        ' if you remove them in the Collection editor
        Private Sub ResetXTDRItems()
            XTDItems.Clear()
        End Sub

        Private Function ShouldSerializeXTDRItems() As Boolean
            Return (XTDItems.Count > 0) ' IsNot Nothing)
        End Function

#End Region

#Region " ***  ZItemCollection Property"

        ' uses   ZItems
        ' Collection(Of ZItem)
        ' ZItemCollectionEditor

        Friend colZ As New ZItems           ' defined in ZITEMS

        <Description("ZItem Extender Collection")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(ZItemCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property ZItemCollection As ZItems
            Get
                Return colZ
            End Get
        End Property


        ' these are pretty much required - they tell NET
        ' when to serialize and how to clear the collection
        ' if you remove them in the Collection editor
        Friend Sub ResetZItemCollection()
            colZ.Clear()
        End Sub

        Friend Function ShouldSerializeZItemCollection() As Boolean
            Return (colZ.Count > 0)
        End Function

#End Region

#Region " ***  ZCollectionBase Property"

        ' uses   ZItems
        ' CollectionBase as collector
        ' ZItemCollectionEditor

        Friend _zCol As New ZColBase       ' defined in ZITEMS

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(ZItemCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property ZCollectionBase As ZColBase
            Get
                Return _zCol
            End Get
        End Property

        Private Sub ResetZCollectionBase()
            _zCol.Clear()
        End Sub

        Private Function ShouldSerializeZCollectionBase() As Boolean
            Return (_zCol.Count > 0)
        End Function

#End Region

#Region " ***  ZObservable Property"

        Friend _zObservable As New ZObservable

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(ZItemCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property ZObserveList As ZObservable
            Get
                Return _zObservable
            End Get

        End Property

        Private Sub ResetZObserveList()
            _zObservable.Clear()

        End Sub

        Private Function ShouldSerializeZObserveList() As Boolean
            Return (_zObservable.Count > 0)
        End Function

#End Region

#Region " ***  XooBars Collection / INameProvider demo "

        ' uses XooBar Items 
        ' XooBarCollection wrapper 
        ' XooBarCollectionEditor...specialized to support INameProvider 

        Friend _XooBars As New XooBarCollection

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(XooBarCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property XooBarsCollection As XooBarCollection
            Get
                Return _XooBars
            End Get

        End Property

        Private Sub ResetXooBarsCollection()
            _XooBars.Clear()
        End Sub

        Private Function ShouldSerializeXooBarsCollection() As Boolean
            Return (_XooBars.Count > 0)
        End Function

#End Region


#Region "   *** INameProvider  *** "

        ' Only XooBar items and XooBar.ZSubItem use the 
        ' name service.
        '    The XooBarCollection provides XooItem names.
        '    ZSubItems are Auto (Handled by the editor).
        '    so the code below is not used
        '
        ' As the property provider, NuControl COULD be the 
        '  second call to get a name for XooBar Items
        '  or ANY of the top level collections.
        '
        ' The property provider for ZSubItems is XooBarItem,
        '   so the second change call would go there.

        ' This would be the only way use the NameProvider method for
        ' trivial collections using List<T> variables otherwise
        ' implementing INP on this class is optional

        ' The demo does not use the procedure below (see above)
        '    but is how/where NuControl could get involved.

        Public Function GetNewName(tName As String) As String Implements INameProvider.GetNewName

            Select Case tName
                Case GetType(XooBar).ToString
                    ' this will only fire if you remove INameProvider interdface
                    ' from Xoobars collection
                    Return _XooBars.GetNewName(tName)

                Case "Et Cetera"
                    ' add other type names here and call the appropriate
                    ' method to get a unique name, ID, Index, whatever

                    Return "NewItem"

                Case Else
                    ' throw an exception if you like or
                    ' return a generic name as a debug signal
                    ' you cant just turn it on in your editor -
                    ' someone has to do the work
                    MessageBox.Show("No Naming Service Priovider for " & tName, "DEMO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return "FUBAR!"
            End Select

        End Function
#End Region

    End Class

End Namespace
