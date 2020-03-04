
Imports System.Drawing.Design
Imports System.ComponentModel
Imports System.Windows.Forms.Design
Imports System.Windows.Forms

Namespace Plutonix.UIDesign

    Public Class UIEnumEditor
        Inherits UITypeEditor
        Implements IDisposable


        ''This is the control to be used in design time DropDown editor
        Private myType As Type

        Private myChkCtl As CheckedListBox
        Private myListCtl As ListBox
        Private EnumDescr As String()

        Private editorService As IWindowsFormsEditorService

        Private FlagType As Boolean = False

        ' need three states
        Protected Friend Property UseDescription As Nullable(Of Boolean)
        Protected Friend Property ControlWidth As Integer

        Public Sub New()
            ControlWidth = 200
            UseDescription = Nothing
        End Sub

        Public Overrides ReadOnly Property IsDropDownResizable As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function GetEditStyle(context As ITypeDescriptorContext) As UITypeEditorEditStyle

            If context IsNot Nothing Then
                ' get as much stuff out of the way here since
                ' EditValue will be busy

                ' get the type and save it
                myType = context.PropertyDescriptor.PropertyType

                If myType.BaseType.UnderlyingSystemType IsNot GetType(System.Enum) Then
                    DisplayError("EnumEditor is only for System.Enum Type Properties!")
                    Return Nothing
                End If

                ' see if it has the FlagsAttribute
                FlagType = myType.IsDefined(GetType(FlagsAttribute), False)

                ' get descriptions or names
                EnumDescr = GetDescriptions(myType)
                ' this can only happen from exceptions:
                If EnumDescr Is Nothing Then UseDescription = False

                ' I am declaring BOTH control types and using an IF block
                ' rather than generic Control.  This is to avoid
                ' CType when it is referenced.  There are enough Ctypes
                ' to gag a pig already just dealing with the Enum 
                If FlagType Then
                    ' config the ctls
                    myChkCtl = New CheckedListBox
                    myChkCtl.CheckOnClick = True
                    myChkCtl.BorderStyle = BorderStyle.Fixed3D

                    If UseDescription Is Nothing Then
                        UseDescription = False
                    ElseIf UseDescription = True Then
                        myChkCtl.Width = ControlWidth
                    End If
                Else
                    myListCtl = New ListBox
                    myListCtl.BorderStyle = BorderStyle.FixedSingle

                    If UseDescription Is Nothing Then
                        UseDescription = True
                    Else
                        ControlWidth = 240
                    End If
                    myListCtl.Width = ControlWidth
                End If
            End If
            ' this is all that NET needed
            Return UITypeEditorEditStyle.DropDown
        End Function

        Public Overrides Function EditValue(context As ITypeDescriptorContext, provider As IServiceProvider, value As Object) As Object
            ' Check if the required objects exist
            If (context IsNot Nothing) AndAlso (context.Instance IsNot Nothing) AndAlso (provider IsNot Nothing) Then

                ' Get the editor used to display the ctrl
                editorService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)

                Dim enumVals As Array = System.Enum.GetValues(myType)
                Dim thisDescr As String

                'DisplayError(String.Format("EnumDescr.Length = {0}  enumVals.Length= {1}", EnumDescr.Length.ToString, EnumDescr.Length.ToString))

                ' "std" Net display mode, except using descriptions
                If FlagType = False Then
                    Dim selectedIndex As Integer

                    For n As Integer = 0 To enumVals.Length - 1
                        If (UseDescription) Then
                            thisDescr = EnumDescr(n)
                        Else
                            thisDescr = myType.GetEnumName(n).ToString
                        End If

                        ' note: items are wrapped in a small class to control the
                        ' the display (ToString) and preserve the value
                        ' Enum.Name will convert back to a value but not a Description!
                        myListCtl.Items.Add(New EnumItem(thisDescr, n))
                        If n = Convert.ToInt32(value) Then
                            selectedIndex = n
                        End If
                    Next
                    ' i dont like setting it in the populate loop
                    myListCtl.SelectedIndex = selectedIndex

                    myListCtl.Height = (myListCtl.Items.Count + 2) * myListCtl.ItemHeight
                    ' Hook to the local event handler
                    AddHandler myListCtl.SelectedIndexChanged, AddressOf ItemSelected

                    ' *****   EDIT VALUE *******
                    editorService.DropDownControl(myListCtl)

                    ' ***** FETCH DATA ****
                    If Not myListCtl.SelectedItem Is Nothing Then
                        ' return the value selected
                        value = System.Enum.ToObject(myType, CType(myListCtl.SelectedItem, EnumItem).Value)
                    End If

                    ' Bitwise flag mode, using CheckedListBox
                Else
                    Dim nVal As Integer
                    Dim bCheck As Boolean

                    ' loop thru all enum bitwise values
                    For n As Integer = 0 To enumVals.Length - 1
                        ' convert to int
                        nVal = Convert.ToInt32(enumVals.GetValue(n))

                        ' ignore 0/None
                        If nVal > 0 Then
                            ' is this item checked?
                            bCheck = ((CType(value, Integer) And nVal) = nVal)

                            If (UseDescription) Then
                                thisDescr = EnumDescr(n)
                            Else
                                ' get the name, using EnumVals because 'n' is an index not enum val
                                thisDescr = myType.GetEnumName(enumVals.GetValue(n)).ToString

                            End If
                            ' Name/Descr + Value wrapper
                            myChkCtl.Items.Add(New EnumItem(thisDescr, nVal), bCheck)

                        End If
                    Next

                    myChkCtl.Height = (myChkCtl.Items.Count + 2) * myChkCtl.ItemHeight
                    'AddHandler myChkCtl.MouseLeave, AddressOf MouseLeave

                    ' ***** USER EDIT *****
                    editorService.DropDownControl(myChkCtl)

                    ' ***** Fetch Data ****
                    Dim newValue As Integer
                    For n As Integer = 0 To myChkCtl.CheckedItems.Count - 1
                        ' get the wrapper value
                        nVal = CType(myChkCtl.CheckedItems.Item(n), EnumItem).Value
                        ' combine
                        newValue = newValue Or nVal
                    Next
                    ' return bitwise item(s) selected, convert to Enum
                    value = System.Enum.ToObject(myType, newValue)
                End If
            End If

            Return value
        End Function

        Private Sub MouseLeave(sender As Object, e As System.EventArgs)
            If editorService IsNot Nothing Then
                editorService.CloseDropDown()
            End If
        End Sub

        Private Sub ItemSelected(ByVal sender As Object, ByVal e As EventArgs)
            If editorService IsNot Nothing Then
                editorService.CloseDropDown()
            End If

        End Sub

        Public Shared Function GetDescriptions(ByVal type As Type) As String()
            Dim n As Integer = 0
            Dim enumValues As Array

            Try
                enumValues = [Enum].GetValues(type)
                Dim Descr(enumValues.Length - 1) As String

                For Each value As [Enum] In enumValues
                    Descr(n) = GetDescription(value)
                    n += 1
                Next
                Return Descr

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return Nothing
            End Try

        End Function

        Public Shared Function GetDescription(ByVal EnumConstant As [Enum]) As String
            Dim fi As Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
            Dim attr() As DescriptionAttribute =
                    DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), 
                    DescriptionAttribute())

            If attr.Length > 0 Then
                Return attr(0).Description
            Else
                Return EnumConstant.ToString()
            End If
        End Function


        Private Const Caption As String = "Plutonix EnumEditor"
        Protected Friend Sub DisplayError(msg As String)
            MessageBox.Show(msg, Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Sub


        ' simple class to store the DisplayName and Value of an enum.
        ' when Descr are used, EnumParse cannot be used to convert
        ' it back to an enum value.  Even when it can, value is faster.
        ' this keeps the value with whatever display text we are using
        ' together
        Protected Friend Class EnumItem

            Public Property Name As String      ' Enum name or Description
            Public Property Value As Integer

            Public Sub New(n As String, v As Integer)
                Name = n
                Value = v
            End Sub

            Public Overrides Function ToString() As String
                Return Name
            End Function

        End Class

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    If myChkCtl IsNot Nothing Then
                        myChkCtl.Dispose()
                    End If
                    If myListCtl IsNot Nothing Then
                        myListCtl.Dispose()
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