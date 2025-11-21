Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand



    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click

        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database=crud_demo_db"

        Try
            conn.Open()
            MessageBox.Show("Connected")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            conn.Close()
        End Try

    End Sub


    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click
        Dim query As String = "INSERT INTO students_tbl (name, age, email) VALUES (@name, @age, @email)"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", TextBoxAge.Text)
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record Inserted Successfully")
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        LoadData()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim query As String = "SELECT * FROM crud_demo_db.students_tbl;"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db")
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim table As New DataTable()
                adapter.Fill(table)
                DataGridView1.DataSource = table
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

                TextBoxName.Text = row.Cells("name").Value.ToString()
                TextBoxAge.Text = row.Cells("age").Value.ToString()
                TextBoxEmail.Text = row.Cells("email").Value.ToString()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub



    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click

        If DataGridView1.CurrentRow Is Nothing Then
            MsgBox("Please select a row first.")
            Exit Sub
        End If

        Dim selectedID As Integer = DataGridView1.CurrentRow.Cells("id").Value

        Dim query As String = "UPDATE students_tbl SET name=@name, age=@age, email=@email WHERE id=@id"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", selectedID)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", TextBoxAge.Text)
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)

                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record Updated Successfully")

                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        LoadData()
    End Sub


    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs) Handles ButtonDelete.Click

        If DataGridView1.CurrentRow Is Nothing Then
            MsgBox("Please select a row first.")
            Exit Sub
        End If

        Dim selectedID As Integer = DataGridView1.CurrentRow.Cells("id").Value

        Dim query As String = "DELETE FROM students_tbl WHERE id=@id"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)

                    cmd.Parameters.AddWithValue("@id", selectedID)
                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Record Deleted Successfully")

                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        LoadData()
    End Sub

End Class
