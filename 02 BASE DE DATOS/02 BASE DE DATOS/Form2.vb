Imports System.Data
Imports System.Data.OleDb
Public Class Form2
    Dim conexion As New OleDbConnection
    Dim comando As New OleDbCommand
    Dim adaptador As New OleDbDataAdapter
    Dim datos As New DataSet

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'para validar
        Try
            conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\JIMMY DELGADO\Desktop\PERSONA1.accdb"
            conexion.Open()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim consulta As String
        Dim nombre As Byte

        If TextBox1.Text.Length = 0 Then
            ErrorProvider1.SetError(TextBox1, "INGRESE USUARIO")
            TextBox1.Focus()
        Else
            ErrorProvider1.SetError(TextBox1, Nothing)
        End If

        If TextBox2.Text.Length = 0 Then
            ErrorProvider2.SetError(TextBox2, "INGRESE CLAVE")

        Else
            ErrorProvider2.SetError(TextBox2, Nothing)
        End If

        If TextBox1.Text <> "" Then
            consulta = "SELECT * FROM USUARIOS where nomUsuario='" & TextBox1.Text & "'"
            consulta = "SELECT * FROM USUARIOS where clave='" & TextBox2.Text & "'"
            adaptador = New OleDbDataAdapter(consulta, conexion)
            datos = New DataSet
            adaptador.Fill(datos, "USUARIOS")
            nombre = datos.Tables("USUARIOS").Rows.Count
        End If

        If nombre <> 0 Then
            TextBox1.Text = datos.Tables("USUARIOS").Rows(0).Item("nomUsuario")
            TextBox2.Text = datos.Tables("USUARIOS").Rows(0).Item("clave")
            Me.Hide()
            Form1.Show()
            MsgBox("BIENVENIDO A LA BASE DE DATOS")
        Else
            MsgBox("Usuario o Clave incorrecto", MsgBoxStyle.Critical)
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox1.Focus()

        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsSeparator(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
            MsgBox("SOLO PUEDE INGRESAR LETRAS ", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If (Char.IsDigit(e.KeyChar)) Then
            e.Handled = False
        ElseIf (Char.IsControl(e.KeyChar)) Then
            e.Handled = False
        ElseIf (Char.IsSeparator(e.KeyChar)) Then
            e.Handled = False
        Else
            e.Handled = True
            MsgBox("SOLO PUEDE INGRESAR NÚMEROS", MsgBoxStyle.Critical)
        End If
    End Sub
End Class
    