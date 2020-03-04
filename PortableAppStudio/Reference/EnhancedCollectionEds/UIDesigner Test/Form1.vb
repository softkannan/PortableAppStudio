Imports Plutonix.UIDesign.RunTimeUIEdTools

Public Class Form1



    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        RunTimeTypeEdit.ShowEditor(Me, NuControl1, "XTDRItems")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        RunTimeTypeEdit.ShowEditor(Me, NuControl1, "ZItemCollection")

    End Sub

    
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
