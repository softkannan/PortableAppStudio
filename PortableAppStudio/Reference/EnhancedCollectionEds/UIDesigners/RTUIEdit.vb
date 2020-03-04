Imports System.Windows.Forms
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Reflection
Imports System.Windows.Forms.Design


Namespace Plutonix.UIDesign

    Public Class RunTimeUIEdTools

        Public Shared Function BaseNameFromType(ItemType As Type) As String
            Return BaseNameFromTypeName(ItemType.ToString)
        End Function

        Public Shared Function BaseNameFromTypeName(ItemTypeName As String) As String
            Return ItemTypeName.Remove(0, ItemTypeName.LastIndexOf(".") + 1)
        End Function


        Public NotInheritable Class RunTimeTypeEdit
            Implements IWindowsFormsEditorService
            Implements IServiceProvider
            Implements ITypeDescriptorContext

            ' AHA moment inspiration:
            'http://stackoverflow.com/a/849778/1070452

            Private ReadOnly myOwner As IWin32Window
            Private ReadOnly myComponent As Object
            Private ReadOnly myProperty As PropertyDescriptor

            ''' <summary>
            ''' 
            ''' </summary>
            ''' <param name="owner">form owner for the UI Dialog</param>
            ''' <param name="component">the class instance to edit</param>
            ''' <param name="propertyName">property name to edit (must have the Editor attribute)</param>
            ''' <remarks></remarks>
            Public Shared Sub ShowEditor(owner As IWin32Window, component As Object, propertyName As String)
                Dim prop As PropertyDescriptor = TypeDescriptor.GetProperties(component)(propertyName)
                ' check if the property exists
                If prop Is Nothing Then
                    Throw New ArgumentException("propertyName")
                End If

                ' get the designated editor
                Dim editor As UITypeEditor = DirectCast(prop.GetEditor(GetType(UITypeEditor)), UITypeEditor)

                ' this will be Null if there is no UIEditor decoration
                If editor IsNot Nothing Then

                    ' it must be one we know is legal 
                    ' this could check a list of types and if the editor was modal instead 
                    If editor.GetType.IsSubclassOf(GetType(EnhancedCollectionEditor)) Or
                            editor.GetType.IsSubclassOf(GetType(CollectionEditor)) Then

                        ' create
                        Dim thisContext As New RunTimeTypeEdit(owner, component, prop)

                        ' get the current value of said property
                        Dim value As Object = prop.GetValue(component)
                        ' Go forth and Edit
                        value = editor.EditValue(thisContext, thisContext, value)
                        If prop.IsReadOnly = False Then
                            ' set the value
                            prop.SetValue(component, value)
                        End If

                    Else
                        Throw New NotImplementedException("Unsupported UIEditor Type")
                    End If
                Else
                    Throw New NotImplementedException("Unsupported UIEditor Type")
                End If
            End Sub

            Private Sub New(owner As IWin32Window, component As Object, prop As PropertyDescriptor)
                myOwner = owner
                myComponent = component
                myProperty = prop
            End Sub


            Public Shared Function GetUIEditor(instance As Object, propName As String) As UITypeEditor

                Return CType(TypeDescriptor.GetProperties(instance)(propName).GetEditor(GetType(UITypeEditor)), 
                                UITypeEditor)

            End Function


#Region "required interface members"

            Public Function GetService(serviceType As Type) As Object Implements IServiceProvider.GetService
                If serviceType = GetType(IWindowsFormsEditorService) Then
                    Return Me
                Else
                    Return Nothing
                End If
            End Function

            Public Function ShowDialog(dialog As System.Windows.Forms.Form) As System.Windows.Forms.DialogResult Implements IWindowsFormsEditorService.ShowDialog
                Return dialog.ShowDialog(myOwner)
            End Function

#End Region

#Region "required empty or illegal members"

            Private ReadOnly Property ITypeDescriptorContext_Container() As IContainer Implements ITypeDescriptorContext.Container
                Get
                    Return Nothing
                End Get
            End Property

            Private ReadOnly Property ITypeDescriptorContext_Instance() As Object Implements ITypeDescriptorContext.Instance
                Get
                    Return myComponent
                End Get
            End Property

            Private Sub ITypeDescriptorContext_OnComponentChanged() Implements ITypeDescriptorContext.OnComponentChanged
            End Sub

            Private Function ITypeDescriptorContext_OnComponentChanging() As Boolean Implements ITypeDescriptorContext.OnComponentChanging
                Return True
            End Function

            Private ReadOnly Property ITypeDescriptorContext_PropertyDescriptor() As PropertyDescriptor Implements ITypeDescriptorContext.PropertyDescriptor
                Get
                    Return myProperty
                End Get
            End Property

            Public Sub CloseDropDown() Implements IWindowsFormsEditorService.CloseDropDown
                Throw New NotImplementedException()
            End Sub

            Public Sub DropDownControl(control As System.Windows.Forms.Control) Implements IWindowsFormsEditorService.DropDownControl
                Throw New NotImplementedException()
            End Sub

#End Region

        End Class

    End Class


End Namespace