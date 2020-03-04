Imports NuControl


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Ziggy1 As NuControl.Ziggy = New NuControl.Ziggy("Ziggy1", -1, "ZText")
        Dim Ziggy2 As NuControl.Ziggy = New NuControl.Ziggy("Ziggy2", -1, "ZText")
        Dim Ziggy3 As NuControl.Ziggy = New NuControl.Ziggy("Ziggy3", -1, "ZText")
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabCol = New System.Windows.Forms.TabPage()
        Me.NuControl1 = New NuControl.NuControl()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.tabEnum = New System.Windows.Forms.TabPage()
        Me.EnumDemoControl1 = New EnumDemoControl()
        Me.ExampleComponent1 = New ExampleComponent()
        Me.TabControl1.SuspendLayout()
        Me.tabCol.SuspendLayout()
        CType(Me.NuControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabEnum.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabCol)
        Me.TabControl1.Controls.Add(Me.tabEnum)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(549, 281)
        Me.TabControl1.TabIndex = 0
        '
        'tabCol
        '
        Me.tabCol.Controls.Add(Me.NuControl1)
        Me.tabCol.Controls.Add(Me.Label1)
        Me.tabCol.Controls.Add(Me.Button1)
        Me.tabCol.Controls.Add(Me.btnEdit)
        Me.tabCol.Location = New System.Drawing.Point(4, 22)
        Me.tabCol.Name = "tabCol"
        Me.tabCol.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCol.Size = New System.Drawing.Size(541, 255)
        Me.tabCol.TabIndex = 0
        Me.tabCol.Text = "Collections"
        '
        'NuControl1
        '
        Me.NuControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NuControl1.Location = New System.Drawing.Point(25, 15)
        Me.NuControl1.Name = "NuControl1"
        Me.NuControl1.Size = New System.Drawing.Size(226, 162)
        Me.NuControl1.TabIndex = 9
        Ziggy1.PropVal = 0
        Ziggy1.ZFoo = "Ziggy's Foo"
        Ziggy2.PropVal = 0
        Ziggy2.ZFoo = "Ziggy's Foo"
        Ziggy3.PropVal = 0
        Ziggy3.ZFoo = "Ziggy's Foo"
        Me.NuControl1.ZCollectionBase.AddRange(New NuControl.ZItem() {CType(Ziggy1, NuControl.ZItem), CType(Ziggy2, NuControl.ZItem), CType(Ziggy3, NuControl.ZItem)})
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(287, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(175, 26)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "UIDesigners at Run Time"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(290, 111)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Edit ZItems"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(290, 60)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(153, 23)
        Me.btnEdit.TabIndex = 6
        Me.btnEdit.Text = "Edit XTDR Items"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'tabEnum
        '
        Me.tabEnum.Controls.Add(Me.EnumDemoControl1)
        Me.tabEnum.Location = New System.Drawing.Point(4, 22)
        Me.tabEnum.Name = "tabEnum"
        Me.tabEnum.Padding = New System.Windows.Forms.Padding(3)
        Me.tabEnum.Size = New System.Drawing.Size(541, 255)
        Me.tabEnum.TabIndex = 1
        Me.tabEnum.Text = "Enum"
        '
        'EnumDemoControl1
        '
        Me.EnumDemoControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.EnumDemoControl1.EnumOfColors = EnumDemoControl.FlagColors.White
        Me.EnumDemoControl1.EnumOfFlaggedStooge = CType(((EnumDemoControl.StoogeFlags.Larry Or EnumDemoControl.StoogeFlags.Moe) _
            Or EnumDemoControl.StoogeFlags.Curly), EnumDemoControl.StoogeFlags)
        Me.EnumDemoControl1.EnumOfNetNormal = EnumDemoControl.Simple.Apple
        Me.EnumDemoControl1.EnumOfSimple = EnumDemoControl.Simple.Apple
        Me.EnumDemoControl1.EnumOfStooge = EnumDemoControl.Stooges.Larry
        Me.EnumDemoControl1.Location = New System.Drawing.Point(83, 32)
        Me.EnumDemoControl1.Name = "EnumDemoControl1"
        Me.EnumDemoControl1.Size = New System.Drawing.Size(205, 177)
        Me.EnumDemoControl1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(549, 281)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.tabCol.ResumeLayout(False)
        CType(Me.NuControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabEnum.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabCol As System.Windows.Forms.TabPage
    Friend WithEvents tabEnum As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents NuControl1 As NuControl.NuControl
    Friend WithEvents ExampleComponent1 As ExampleComponent
    Friend WithEvents EnumDemoControl1 As EnumDemoControl


End Class
