Imports System.Data
Imports System.Data.OleDb
Public Class Form1
    Dim codigo As String
    Public Sub generarcodigo()
        Me.codigo = Me.TextBox2.Text.Substring(0, 2) + TextBox3.Text.Substring(0, 2) + "19"
        TextBox1.Text = codigo
    End Sub
    Dim conexion As New OleDbConnection
    Private Sub Form1_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\JIMMY DELGADO\Desktop\PERSONA1.accdb"
            conexion.Open()
            'mostrar()
            MsgBox("conexion exitosa")
        Catch ex As Exception
            MsgBox("no se realizo la conexion")

        End Try
        Dim adaptador As New OleDbDataAdapter("select*from PERSONA", conexion)
        Dim datos As New DataSet
        adaptador.Fill(datos)
        DataGridView1.DataSource = datos.Tables(0)
    End Sub
    Private Sub mostrar()
        Dim oda As New OleDbDataAdapter
        Dim ods As New DataSet
        Dim consulta As String
        consulta = "select *from PERSONA"
        oda = New OleDbDataAdapter(consulta, conexion)
        ods.Tables.Add("PERSONA")
        oda.Fill(ods.Tables("PERSONA"))
        DataGridView1.DataSource = ods.Tables("PERSONA")

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim comando As New OleDbCommand
        If TextBox2.Text.Length = 0 Then
            ErrorProvider1.SetError(TextBox2, "INGRESE NOMBRE")
            TextBox2.Focus()
        Else
            ErrorProvider1.SetError(TextBox2, Nothing)
        End If

        If TextBox3.Text.Length = 0 Then
            ErrorProvider1.SetError(TextBox3, "INGRESE APELLIDO")
            TextBox3.Focus()
        Else
            ErrorProvider1.SetError(TextBox3, Nothing)
        End If
        If TextBox4.Text.Length = 0 Then
            ErrorProvider1.SetError(TextBox4, "INGRESE TELEFONO")
            TextBox4.Focus()
        Else
            ErrorProvider1.SetError(TextBox4, Nothing)
        End If
        If TextBox2.Text <> "" Then
            If TextBox3.Text <> "" Then
                If TextBox4.Text <> "" Then
                    If TextBox5.Text <> "" Then

                        TextBox1.Text = "ID" & TextBox3.Text.Substring(0, 3) & "19"

                        comando = New OleDbCommand("INSERT INTO PERSONA(Id,NOMBRE,APELLIDO,TELEFONO,CIUDAD)VALUES(TextBox1,TextBox2,TextBox3,TextBox4,TextBox5)", conexion)
                        comando.Parameters.AddWithValue("@Id", TextBox1.Text)
                        comando.Parameters.AddWithValue("@NOMBRE", TextBox2.Text)
                        comando.Parameters.AddWithValue("@APELLIDO", TextBox3.Text)
                        comando.Parameters.AddWithValue("@TELEFONO", TextBox4.Text)
                        comando.Parameters.AddWithValue("@CIUDAD", TextBox5.Text)
                        comando.ExecuteNonQuery()
                        mostrar()
                        MsgBox("GRABADO EXITOSAMENTE")
                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = ""
                        TextBox4.Text = ""
                        TextBox5.Text = ""
                        TextBox2.Focus()
                    Else
                        MsgBox("LLENE TODOS LOS DATOS")
                        TextBox2.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()
                        TextBox5.Clear()
                        TextBox2.Focus()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub buscar()
        If TextBox1.Text = "" Then
            MsgBox("INGRESE UN CODIGO A BUSCAR")
        Else

            Dim adaptador As New OleDbDataAdapter("SELECT * FROM PERSONA where Id like'" & TextBox1.Text & "%'", conexion)
            Dim datos As New DataSet

            adaptador.Fill(datos, "PERSONA")
            TextBox1.Text = datos.Tables("PERSONA").Rows(0).Item("Id")
            TextBox2.Text = datos.Tables("PERSONA").Rows(0).Item("NOMBRE")
            TextBox3.Text = datos.Tables("PEESONA").Rows(0).Item("APELLIDO")
            TextBox4.Text = datos.Tables("PERSONA").Rows(0).Item("TELEFONO")
            TextBox5.Text = datos.Tables("PERSONA").Rows(0).Item("CIUDAD")

        End If
    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        generarcodigo()
    End Sub
    Private Sub modificar()
        Dim comando As New OleDbCommand("update PERSONA set Id=@Id,NOMBRE=@NOMBRE,APELLIDO=@APELLIDO,TELEFONO=@TELEFONO,CIUDAD=@CIUDAD  where Id=@Id", conexion)
        Dim datos As New DataSet

        Try
            comando.Parameters.Add(New OleDbParameter("@Id", TextBox1.Text))
            comando.Parameters.Add(New OleDbParameter("@NOMBRE", TextBox2.Text))
            comando.Parameters.Add(New OleDbParameter("@APELLIDO", TextBox3.Text))
            comando.Parameters.Add(New OleDbParameter("@TELEFONO", TextBox4.Text))
            comando.Parameters.Add(New OleDbParameter("@CIUDAD", TextBox5.Text))
            comando.ExecuteNonQuery()

            mostrar()

            MsgBox("MODIFICADO EXITOSAMENTE")
        Catch ex As Exception
            MsgBox("NO HA MODIFICADO")
        End Try

    End Sub


    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        modificar()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'BUSCAR REGISTRO MEDIANTE LA ID DE LA BASE DE DATOS Y SACAR A LAS CAJAS DE TEXTO
        Try
            Dim adaptador1 As New OleDbDataAdapter("SELECT *FROM PERSONA where Id like '" & TextBox1.Text & "%'", conexion)
            Dim registro1 As New DataSet
            adaptador1.Fill(registro1)
            TextBox2.Text = registro1.Tables(0).Rows(0)("NOMBRE").ToString
            TextBox3.Text = registro1.Tables(0).Rows(0)("APELLIDO").ToString
            TextBox4.Text = registro1.Tables(0).Rows(0)("TELEFONO").ToString
            TextBox5.Text = registro1.Tables(0).Rows(0)("CIUDAD").ToString
        Catch ex As Exception
            MsgBox("CODIGO NO EXISTE")
        End Try
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Dim comando As New OleDbCommand("delete from PERSONA where Id=@Id", conexion)
        Try
            comando.Parameters.Add(New OleDbParameter("@Id", TextBox1.Text))
            comando.ExecuteNonQuery()
            mostrar()

            MsgBox("ELIMINADO EXITOSAMENTE")
        Catch ex As Exception
            MsgBox("NO SE PUEDE ELIMINAR")
        End Try
    End Sub
   
    Private Sub TextBox2_keypress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress, TextBox2.KeyPress
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

    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
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
    Private Sub eliminar()
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        TextBox1.Visible = False
        TextBox2.Visible = False
        TextBox3.Visible = False
        TextBox4.Visible = False
        TextBox5.Visible = False
        Button1.Visible = False
        Button2.Visible = False
        Button3.Visible = False
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        DataGridView1.Rows(e.RowIndex).Selected = True
        With DataGridView1
            Dim Id As String
            Dim NOMBRE As String
            Dim APELLIDO As String
            Dim TELEFONO As String
            Dim CIUDAD As String

            Id = .Rows(.CurrentCellAddress.Y).Cells("Id").Value
            NOMBRE = .Rows(.CurrentCellAddress.Y).Cells("NOMBRE").Value
            APELLIDO = .Rows(.CurrentCellAddress.Y).Cells("APELLIDO").Value
            TELEFONO = .Rows(.CurrentCellAddress.Y).Cells("TELEFONO").Value
            CIUDAD = .Rows(.CurrentCellAddress.Y).Cells("CIUDAD").Value

            TextBox1.Text = Id.ToString
            TextBox2.Text = NOMBRE.ToString
            TextBox3.Text = APELLIDO.ToString
            TextBox4.Text = TELEFONO.ToString
            TextBox5.Text = CIUDAD.ToString
            mostrar()
        End With


    End Sub

   
End Class

