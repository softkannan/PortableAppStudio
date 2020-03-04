Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Collections.ObjectModel
Imports System.Windows.Forms
Imports System.Windows.Forms.Design
Imports System


Namespace Plutonix.UIDesign

    <System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
     Public MustInherit Class ControlCollectionDialogUIEditor
        Inherits UITypeEditorBase

        ''This is the control to be used in design time DropDown editor
        Private WithEvents myForm As ControlsDialogForm

        ' these allow you to exclude the parent form and/or Me when used by a control
        Protected Friend Property ExcludeForm As Boolean = False
        'Protected ExcludeSelf As Boolean = True

        ' this is a list of Allowed Types
        ' seed this in the ctor in a new class which inherits from UITypeEditorBase:
        '       typeList.Add(GetType(TextBox))
        '       typeList.Add(GetType(ComboBox))
        ' etc
        Protected Friend typeIncludeOnly As New List(Of System.Type)

        Protected Friend typeExclude As New List(Of System.Type)

        Public Sub New()

        End Sub

        ' returns the control to WIN/NET when it asks
        Protected Overrides Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As Control
            'create the control

            myForm = New ControlsDialogForm()
            'it's a form in this case so set its title
            myForm.Text = "Edit Controls Collection: " & PropertyName
            'give a reference to the control back to the base class method
            'the control has not been shown at this point, just created
            Return myForm

        End Function


        Protected Overrides Function SetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle
            Return UITypeEditorEditStyle.Modal
        End Function

        Protected Overrides Function GetEditedValue(ByVal EditControl As Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object
            'if for some reason no control has been created just return the previous (current) value for the property

            If EditControl Is Nothing Then Return OldValue
            'Check that the cancel button hasn't been pressed on the control
            If myForm.IsCancelled Then
                Return OldValue
            Else
                'Return the new value for the property

                'first test if anything has been checked
                Dim tCollection As New Collection(Of Control)
                If myForm.clbControls.CheckedItems.Count <> 0 Then
                    'copy the Controls represented by the checked items in the CheckedListBox to a Collection
                    'the Collection is the same type as the property in the custom component

                    'create a temporary Collection
                    For Each c As Control In myForm.clbControls.CheckedItems
                        'cycle through the checked controls only and add each one to the temporary collection
                        tCollection.Add(c)
                    Next
                End If
                'return the temporary collection to the property
                Return tCollection
            End If
        End Function


        Protected Overrides Sub LoadValues(ByVal context As System.ComponentModel.ITypeDescriptorContext,
                                ByVal provider As System.IServiceProvider, ByVal value As Object)
            'Load the AvailableControls checked list with all form controls visible through the Designer
            Dim bAdd As Boolean = True
            Dim thisCtl As Control = Nothing

            If TypeOf context.Instance Is Control Then
                Throw New InvalidOperationException("This Designer intended for use by Components")
            End If

            Dim tCollection As Collection(Of Control) = CType(value, Collection(Of Control))
            For Each obj As Object In context.Container.Components
                'Cycle through the components owned by the form in the designer
                bAdd = True

                ' exclude other components - this weeds out DataGridViewCOlumns which
                ' can only be used by a DataGridView
                If TypeOf obj Is Control Then
                    thisCtl = CType(obj, Control)

                    If ExcludeForm Then
                        bAdd = Not (TypeOf thisCtl Is Form)
                    End If

                    ' custom Include only these list
                    If (typeIncludeOnly IsNot Nothing) AndAlso (typeIncludeOnly.Count > 0) Then
                        If typeIncludeOnly.Contains(thisCtl.GetType) = False Then
                            bAdd = False
                        End If
                    End If

                    ' custom exclude list 
                    If (typeExclude IsNot Nothing) AndAlso (typeExclude.Count > 0) Then
                        If typeExclude.Contains(thisCtl.GetType) Then
                            bAdd = False
                        End If
                    End If

                    Dim bCheck As Boolean
                    Dim ndx As Integer
                    If bAdd Then
                        bCheck = tCollection.Contains(thisCtl)
                        ndx = myForm.clbControls.Items.Add(thisCtl)
                        myForm.clbControls.SetItemChecked(ndx, bCheck)
                    End If

                End If

            Next

        End Sub

    End Class

    Friend Class ControlsDialogForm
        Inherits System.Windows.Forms.Form

        Friend WithEvents OK_Button As System.Windows.Forms.Button
        Friend WithEvents Cancel_Button As System.Windows.Forms.Button
        Friend clbControls As System.Windows.Forms.CheckedListBox
        Friend WithEvents btnAll As System.Windows.Forms.Button

        Friend IsCancelled As Boolean = False


        Public Sub New()
            InitializeComponent()
        End Sub

        <System.Diagnostics.DebuggerStepThrough()> _
          Private Sub InitializeComponent()
            Me.OK_Button = New System.Windows.Forms.Button
            Me.Cancel_Button = New System.Windows.Forms.Button
            Me.btnAll = New System.Windows.Forms.Button
            Me.clbControls = New System.Windows.Forms.CheckedListBox
            Me.SuspendLayout()
            '
            'OK_Button
            '
            Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.OK_Button.Location = New System.Drawing.Point(217, 267)
            Me.OK_Button.Name = "OK_Button"
            Me.OK_Button.Size = New System.Drawing.Size(67, 23)
            Me.OK_Button.TabIndex = 0
            Me.OK_Button.Text = "OK"
            '
            'Cancel_Button
            '
            Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Cancel_Button.Location = New System.Drawing.Point(142, 267)
            Me.Cancel_Button.Name = "Cancel_Button"
            Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
            Me.Cancel_Button.TabIndex = 1
            Me.Cancel_Button.Text = "Cancel"
            '
            'btnAll
            '
            Me.btnAll.Anchor = System.Windows.Forms.AnchorStyles.None
            Me.btnAll.Location = New System.Drawing.Point(12, 267)
            Me.btnAll.Name = "btnAll"
            Me.btnAll.Size = New System.Drawing.Size(67, 23)
            Me.btnAll.TabIndex = 11
            Me.btnAll.Text = "Select All"
            '
            'AvailableControls
            '
            Me.clbControls.CheckOnClick = True
            Me.clbControls.FormattingEnabled = True
            Me.clbControls.Location = New System.Drawing.Point(12, 12)
            Me.clbControls.Name = "AvailableControls"
            Me.clbControls.Size = New System.Drawing.Size(272, 244)
            Me.clbControls.TabIndex = 10
            '
            'ControlCollectionUITypeEditorForm
            '
            Me.AcceptButton = Me.OK_Button
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.Cancel_Button
            Me.ClientSize = New System.Drawing.Size(296, 302)
            Me.Controls.Add(Me.Cancel_Button)
            Me.Controls.Add(Me.OK_Button)
            Me.Controls.Add(Me.btnAll)
            Me.Controls.Add(Me.clbControls)

            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
            Me.KeyPreview = True
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "ControlCollectionUIEditorForm"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Edit Controls Collection: "
            Me.ResumeLayout(False)

        End Sub

        Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
            Me.IsCancelled = False

            'Hide the form, don't close it, so the property editor can refer to the selections made
            Me.Hide()
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
            Me.IsCancelled = True
            'Hide the form, don't close it, so the property editor can refer to the selections made
            Me.Hide()
        End Sub

        Private Sub btnAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAll.Click
            For n As Integer = 0 To clbControls.Items.Count - 1

                clbControls.SetItemChecked(n, True)

            Next
        End Sub
    End Class

    <System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name:="FullTrust")> _
    Public MustInherit Class ControlCollectionDropDownUIEditor
        Inherits UITypeEditorBase
        Implements IDisposable

        ''This is the control to be used in design time DropDown editor
        Private myCtl As CheckedListBox

        ' these allow you to exclude the parent form and/or Me when used by a control
        Protected Friend Property ExcludeForm As Boolean = False
        'Protected ExcludeSelf As Boolean = True

        ' this is a list of Allowed Types
        ' seed this in the ctor in a new class which inherits from UITypeEditorBase:
        '       typeList.Add(GetType(TextBox))
        '       typeList.Add(GetType(ComboBox))
        ' etc
        Protected Friend typeIncludeOnly As New List(Of System.Type)

        Protected Friend typeExclude As New List(Of System.Type)

        Protected Friend Property CheckControlWidth As Integer = 280

        Public Sub New()

        End Sub

        Protected Overrides Function SetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle
            Return UITypeEditorEditStyle.Modal
        End Function

        ' returns the control to WIN/NET when it asks
        Protected Overrides Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As Control
            'create the control


            myCtl = New CheckedListBox
            myCtl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            myCtl.CheckOnClick = True

            myCtl.Height = 200
            ' check for stupidity
            If CheckControlWidth < 400 Then
                myCtl.Width = CheckControlWidth
            End If

            'give a reference to the control back to the base class method
            'the control has not been shown at this point, just created

            Return myCtl

        End Function


        Protected Overrides Function GetEditedValue(ByVal EditControl As Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object
            'if for some reason no control has been created just return the previous (current) value for the property

            If EditControl Is Nothing Then Return OldValue

            'Return the new value for the property

            'first test if anything has been checked
            Dim tCollection As New Collection(Of Control)
            If myCtl.CheckedItems.Count <> 0 Then
                'copy the Controls represented by the checked items in the CheckedListBox to a Collection
                'the Collection is the same type as the property in the custom component

                'create a temporary Collection
                'For Each c As Control In myCtl.CheckedItems
                '    'cycle through the checked controls only and add each one to the temporary collection
                '    tCollection.Add(c)
                'Next

                For n As Integer = 0 To myCtl.CheckedItems.Count - 1
                    tCollection.Add(CType(myCtl.CheckedItems.Item(n), ListItem).[Control])
                Next
            End If

            'return the temporary collection to the property
            Return tCollection

        End Function

        Protected Overrides Sub LoadValues(ByVal context As System.ComponentModel.ITypeDescriptorContext, _
                ByVal provider As System.IServiceProvider, ByVal value As Object)

            'Load the AvailableControls checked list with all form controls visible through the Designer
            Dim bAdd As Boolean = True
            Dim thisCtl As Control = Nothing


            Dim tCollection As Collection(Of Control) = CType(value, Collection(Of Control))


            For Each obj As Object In context.Container.Components
                'Cycle through the components owned by the form in the designer
                bAdd = True

                ' exclude other components - this weeds out DataGridViewColumns which
                ' can only be used by a DataGridView
                If TypeOf obj Is Control Then
                    thisCtl = CType(obj, Control)

                    If ExcludeForm Then
                        bAdd = Not (TypeOf thisCtl Is Form)
                    End If

                    ' custom Include only these list
                    If (typeIncludeOnly IsNot Nothing) AndAlso (typeIncludeOnly.Count > 0) Then
                        If typeIncludeOnly.Contains(thisCtl.GetType) = False Then
                            bAdd = False
                        End If
                    End If

                    ' custom exclude list 
                    If (typeExclude IsNot Nothing) AndAlso (typeExclude.Count > 0) Then
                        If typeExclude.Contains(thisCtl.GetType) Then
                            bAdd = False
                        End If
                    End If


                    Dim bCheck As Boolean
                    Dim ndx As Integer
                    If bAdd Then
                        bCheck = tCollection.Contains(thisCtl)

                        ndx = myCtl.Items.Add(New ListItem(thisCtl))
                        myCtl.SetItemChecked(ndx, bCheck)
                    End If
                End If

            Next

            'If value IsNot Nothing Then
            '    'If the property currently has anything in its collection then check those items in the checked list box
            '    'create a temporary Collection that holds the current controls held by the property

            '    Dim ndx As Integer
            '    Dim tCollection As Collection(Of Control) = CType(value, Collection(Of Control))
            '    For Each c As Control In tCollection
            '        'cycle through the current controls held by the property
            '        ndx = myCtl.Items.IndexOf(c)
            '        If ndx > -1 Then
            '            'if the control has been found in the CheckedListBox then set its cehcked state to true
            '            myCtl.SetItemChecked(ndx, True)
            '        End If
            '    Next
            'End If

        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If myCtl IsNot Nothing Then
                        myCtl.Dispose()
                    End If
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace