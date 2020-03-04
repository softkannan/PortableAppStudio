Option Strict On
Imports System.ComponentModel
Imports System.ComponentModel.Design


Namespace NuControl

    ' VBCollection Test

    Public Class VBCollection
        Private mycol As New Microsoft.VisualBasic.Collection


        Public Sub Add(z As ZItem)
            mycol.Add(z)
        End Sub

        Public ReadOnly Property Item(ndx As Integer) As ZItem
            Get
                Return CType(mycol(ndx), ZItem)
            End Get

        End Property

        Public ReadOnly Property InnerList As Microsoft.VisualBasic.Collection
            Get
                Return mycol
            End Get

        End Property

        Public Function Count() As Integer
            Return mycol.Count
        End Function

        Public Sub Clear()
            mycol.Clear()
        End Sub

    End Class

    Partial Public Class NuControl


        Friend VBCol As New VBCollection


        <Description("VB Collection")> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <Editor(GetType(VBCollectionEditor), GetType(System.Drawing.Design.UITypeEditor))>
        Public ReadOnly Property VBColl As Microsoft.VisualBasic.Collection
            Get
                Return VBCol.InnerList
            End Get
        End Property

        Private Function ShouldSerializeVBColl() As Boolean
            Return VBColl.Count > 0
        End Function

        Private Sub ResetVBColl()
            VBColl.Clear()
        End Sub


    End Class





End Namespace
