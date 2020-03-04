' Original Code: Saeed Serpooshan, Jan 2007
' Adapted By: Mike Conroy, May 2012
'========================================================================
' MadControlCollectionUITypeEditor: MadBadger Control Collection UITypeEditor
' Copyright (C) 2012-2013  Mike Conroy
' 
' This program is free software; you can redistribute it and/or
' modify it under the terms of the The Code Project Open License (CPOL)
' as published by the CodeProject; either version 1.02
' of the License, or (at your option) any later version.
' 
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' CPOL Open License for more details.
' 
' You should have received a copy of the The Code Project Open License
' along with this program; if not, it can be accessed at the following location
' http://www.codeproject.com/info/cpol10.aspx
'========================================================================

Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Windows.Forms.Design

Namespace Plutonix.UIDesign



    ''' <summary>
    ''' This is a UITypeEditor base class useful for simple editing of control properties 
    ''' in a DropDown or a ModalDialogForm window at design mode (in VisualStudio.Net IDE). 
    ''' To use this, inherit a class from it and add this attribute to your control property(ies): 
    ''' 'Editor(GetType(MyPropertyEditor), GetType(System.Drawing.Design.UITypeEditor))  
    ''' </summary>
    Public MustInherit Class UITypeEditorBase
        Inherits UITypeEditor

#Region "Fields"
        Protected IEditorService As IWindowsFormsEditorService
        Protected WithEvents EditControl As Control
        Protected m_EscapePressed As Boolean
        Protected Friend Property Caption As String = "Plutonix UITypeEditor"
#End Region

#Region "Abstract Methods"
        ' ''' <summary>
        ' ''' The driven class should provide its edit Control to be shown in the 
        ' ''' DropDown or DialogForm window by means of this function. 
        ' ''' If specified control be a Form, it is shown in a Modal Form, otherwise, it is shown as in a DropDown window. 
        ' ''' This edit control should return its final value at GetEditedValue() method. 
        ' ''' </summary>
        Protected MustOverride Function GetEditControl(ByVal PropertyName As String, ByVal CurrentValue As Object) As Control

        ''' <summary>The driven class should return the New Value for edited property at this function.</summary>
        ''' <param name="EditControl">
        ''' The control shown in DropDown window and used for editing. 
        ''' This is the control you pass in GetEditControl() function.
        ''' </param>
        ''' <param name="OldValue">The original value of the property before editing through the DropDown window.</param>
        Protected MustOverride Function GetEditedValue(ByVal EditControl As Control, ByVal PropertyName As String, ByVal OldValue As Object) As Object

        ''' <summary>
        ''' Called by EditValue before the dropdown control or modal form is shown, should ensure that the control has the appropriate value(s) loaded; such as the current value or defaults if there is no current value, or just left as a blank method if not relevant
        ''' </summary>
        Protected MustOverride Sub LoadValues(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object)

        'Protected MustOverride Overloads Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle

        Protected MustOverride Function SetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle

#End Region

#Region "Public Methods"

        Public NotOverridable Overrides Function GetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle
            Return SetEditStyle(context)
        End Function


        ' this was throwing nonsense exceptions when called from the 
        ' DropDownEdit version.  I think due to casting between Form and Control for C
        ' just as easy to require the client to return the EditStyle
        'Public Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        '    Dim c As Control = Nothing

        '    Try
        '        Dim propName As String = ""
        '        Dim propValue As Object = Nothing
        '        If context IsNot Nothing Then
        '            propName = context.PropertyDescriptor.Name
        '            propValue = context.PropertyDescriptor.GetValue(context.Instance)
        '        End If

        '        c = GetEditControl(propName, propValue)
        '    Catch ex As Exception
        '        ' saaed wrapped this in an exception for a reason, but
        '        ' collection types are ALWAYS throwing it for DropDowns
        '        ' but they continue to work fine.
        '        ' so I am going to eat it

        '        MessageBox.Show(ex.Message & " * " & (c Is Nothing).ToString)
        '    End Try
        '    If (TypeOf c Is System.Windows.Forms.Form) Then
        '        Return UITypeEditorEditStyle.Modal          'Using a Modal Form
        '    Else
        '        'Using a DropDown Window (This is the default style)
        '        Return UITypeEditorEditStyle.DropDown
        '    End If

        'End Function


        'Displays the Custom UI (a DropDown Control or a Modal Form) for value selection.
        Public Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
            Try
                If context IsNot Nothing AndAlso provider IsNot Nothing Then

                    'Uses the IWindowsFormsEditorService to display a drop-down UI in the Properties window:
                    IEditorService = DirectCast(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
                    If IEditorService IsNot Nothing Then

                        Dim PropName As String = context.PropertyDescriptor.Name
                        EditControl = Me.GetEditControl(PropName, value)         'get Edit Control from driven class

                        Me.LoadValues(context, provider, value)


                        If EditControl IsNot Nothing Then

                            m_EscapePressed = False         'we should set this flag to False before showing the control

                            'show given EditControl
                            ' => it will be closed if user clicks on outside area or we invoke IEditorService.CloseDropDown()

                            If TypeOf EditControl Is Form Then
                                IEditorService.ShowDialog(CType(EditControl, Form))
                            Else

                                IEditorService.DropDownControl(EditControl)

                            End If


                            If m_EscapePressed Then 'return the Old Value (because user press Escape)
                                Return value
                            Else 'get new (edited) value from driven class and return it
                                Return GetEditedValue(EditControl, PropName, value)
                            End If

                        End If 'm_EditControl

                    End If 'IEditorService

                End If 'context And provider

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Return MyBase.EditValue(context, provider, value)

        End Function

        ''' <summary>
        ''' Provides the interface for this UITypeEditor to display Windows Forms or to 
        ''' display a control in a DropDown area from the property grid control in design mode.
        ''' </summary>
        Public Function GetIWindowsFormsEditorService() As IWindowsFormsEditorService
            Return IEditorService
        End Function

        ''' <summary>Close DropDown window to finish editing</summary>
        Public Sub CloseDropDownWindow()
            If IEditorService IsNot Nothing Then IEditorService.CloseDropDown()
        End Sub
#End Region

#Region "Private Methods"
        Private Sub m_EditControl_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) _
          Handles EditControl.PreviewKeyDown
            If e.KeyCode = Keys.Escape Then m_EscapePressed = True
        End Sub
#End Region


        Protected Friend Sub DisplayError(msg As String)
            MessageBox.Show(msg, Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Sub

        Protected Friend Class ListItem
            Public Property [Control] As Control
            Public Property Name As String

            Public Sub New(c As Control)
                [Control] = c
                Name = c.Name
            End Sub

            Public Overrides Function ToString() As String
                Return Name
            End Function

        End Class

    End Class

End Namespace