Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click

        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database= crud_demo_db"

        Try
            conn.Open()
            MessageBox.Show("Connected")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            conn.Close()

        End Try

    End Sub

    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click
        Dim query As String = "INSERT INTO students_tbl (name, age, email) VALUES (@name, @age ,@email)"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database= crud_demo_db")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", TextBoxAge.Text)
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show(" Record Insert Succesfully ")
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query As String = "SELECT * FROM crud_demo_db.students_tbl;"
        Try

            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database= crud_demo_db")
                Dim adapter As New MySqlDataAdapter(query, conn) 'get from database'
                Dim table As New DataTable() 'table object'
                adapter.Fill(table) 'from adapter to table object
                DataGridView1.DataSource = table 'display to datagridview

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class
