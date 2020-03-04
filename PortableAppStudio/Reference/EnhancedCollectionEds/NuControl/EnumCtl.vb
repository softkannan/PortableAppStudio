Imports System.ComponentModel

Imports Plutonix.UIDesign
Imports System.Drawing.Design


Public Class EnumDemoControl
    Inherits Panel

    ' bitwise/flag enum
    <Flags>
    Public Enum FlagColors
        None = 0
        Red = 1
        White = 2
        Blue = 4
        Green = 8
        Yellow = 16
    End Enum

    ' plain enum with descriptions
    Public Enum Stooges
        <Description("Larry - Funny one")> Larry
        <Description("Moe - 'Smart' One")> Moe
        <Description("Curly - Sore One")> Curly
        <Description("Shemp - One with bad haircut")> Shemp
        <Description("CurlyJoe - Last one")> CurlyJoe
    End Enum

    ' flag enum with descriptions
    <Flags>
    Public Enum StoogeFlags
        <Description("No Stooges")> None = 0
        <Description("Larry - Funny one")> Larry = 1
        <Description("Moe - 'Smart' One")> Moe = 2
        <Description("Curly - Sore One")> Curly = 4
        <Description("Shemp - One with bad haircut")> Shemp = 8
        <Description("CurlyJoe - Last one")> CurlyJoe = 16
    End Enum

    ' standard
    Public Enum Simple
        Apple
        Orange
        Cherry
        Grape
    End Enum

 

    <Editor(GetType(UIEnumEditor), GetType(UITypeEditor))>
    Public Property EnumOfColors As FlagColors

    <Editor(GetType(UIEnumEditor), GetType(UITypeEditor))>
    Public Property EnumOfStooge As Stooges

    <Editor(GetType(EnumFruitEditor), GetType(UITypeEditor))>
    Public Property EnumOfSimple As Simple

    <Editor(GetType(EnumFruitEditor), GetType(UITypeEditor))>
    Public Property EnumOfFlaggedStooge As StoogeFlags

    Public Property EnumOfNetNormal As Simple

    ' one enum type ed for all of them
    Public Class EnumFruitEditor
        Inherits UIEnumEditor

        Public Sub New()
            MyBase.New()

            Me.UseDescription = True
            Me.ControlWidth = 200

        End Sub

    End Class



End Class
