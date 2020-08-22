Imports System.Linq.Expressions
Imports MySql.Data.MySqlClient
Public Class frmWorld
    Dim conexion As New MySqlConnection("server=localhost; database=world; Uid=root; Pwd=; ")
    Dim adaptador As New MySqlDataAdapter
    Dim ds As New DataSet
    Dim cmd As New MySqlCommand
    Dim dt As DataTable


    Sub refrescarGrd()
        Dim consulta As String
        consulta = "SELECT country.Name AS 'Pais', city.Name AS 'Ciudad', country.Population AS 'Población', country.IndepYear AS 'Año Independencia' FROM country, city"
        adaptador = New MySqlDataAdapter(consulta, conexion)
        ds = New DataSet
        adaptador.Fill(ds, "world")
        grdListado.DataSource = ds.Tables(0)
    End Sub

    Dim consult As New MySqlCommand("SELECT COUNT(Name) FROM country", conexion)

    Private Sub cantPaises()
        Dim dr As MySqlDataReader
        Dim cantPaises As String = "SELECT COUNT(Name) FROM country"
        cmd = New MySqlCommand(cantPaises, conexion)
        dr = cmd.ExecuteReader
        dr.Read()
        txtCantPaises.Text = dr(0)
        dr.Close()
    End Sub

    Private Sub cantCiudades()
        Dim dr As MySqlDataReader
        Dim cantPaises As String = "SELECT COUNT(Name) FROM city"
        cmd = New MySqlCommand(cantPaises, conexion)
        dr = cmd.ExecuteReader
        dr.Read()
        txtCantCiudades.Text = dr(0)
        dr.Close()
    End Sub

    Private Sub cantIdiomas()
        Dim dr As MySqlDataReader
        Dim cantPaises As String = "SELECT COUNT(Language) FROM countrylanguage"
        cmd = New MySqlCommand(cantPaises, conexion)
        dr = cmd.ExecuteReader
        dr.Read()
        txtCantIdiomas.Text = dr(0)
        dr.Close()
    End Sub

    Public Sub Filtrar(Valor As String)
        Dim consultafiltrar As String = "SELECT country.Name AS 'Pais', city.Name AS 'Ciudad', country.Population AS 'Población', country.IndepYear AS 'Año Independencia' FROM country, city WHERE city.name LIKE '%" & Valor & "%'"
        Dim cmd As New MySqlCommand(consultafiltrar, conexion)
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd)

        da.Fill(dt)

        grdListado.DataSource = dt
    End Sub

    Private Sub frmWorld_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            conexion.Open()
            refrescarGrd()
            cantPaises()
            cantCiudades()
            cantIdiomas()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Filtrar(txtBuscar.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
