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

Imports System.Windows.Forms
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports Plutonix.UIDesign


Public Class ExampleComponent
    Inherits Component

    Private _TargetControls As New Collection(Of Control)

    'EditorAttribute provides the name of the Editor required
    <EditorAttribute(GetType(ExampleDropDownControlCollectionUIEditor),
        GetType(System.Drawing.Design.UITypeEditor))>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property TargetControls() As Collection(Of Control)
        Get
            Return _TargetControls
        End Get
        Set(ByVal value As Collection(Of Control))
            'Validity checking, event raising etc. deliberately left out for simplicity
            _TargetControls = value
        End Set
    End Property

    Public Sub ResetTargetControls()
        _TargetControls = Nothing
    End Sub

    Public Function ShouldSerializeTargetControls() As Boolean
        Return (_TargetControls) IsNot Nothing
    End Function

End Class

' this one fires Mike's Form Editor
Public Class ExampleFormControlCollectionUIEditor
    Inherits ControlCollectionDialogUIEditor

    Public Sub New()
        MyBase.new()

        MyBase.ExcludeForm = False

        MyBase.typeExclude.Add(GetType(RadioButton))
        MyBase.typeExclude.Add(GetType(Label))

    End Sub


End Class

' this one uses a DropDown CheckListbox
' to switch, just change the EditorAttribute on TargetControls,
' then recompile so VS can see the changes here - this is a DESIGN
' time component.
' Note that this one DOES NOT exclude the form.
Public Class ExampleDropDownControlCollectionUIEditor
    Inherits ControlCollectionDropDownUIEditor

    Public Sub New()
        MyBase.New()

        MyBase.ExcludeForm = False

        MyBase.CheckControlWidth = 200

        MyBase.typeExclude.Add(GetType(RadioButton))
        MyBase.typeExclude.Add(GetType(Label))

    End Sub


End Class