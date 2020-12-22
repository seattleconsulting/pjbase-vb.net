Imports System.Data.SqlClient

Public Class sampleform
    Private Sub sampleform_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("大島町")
        ComboBox1.Items.Add("西多摩郡奥多摩町")
        ComboBox1.Items.Add("西多摩郡日の出町")
        ComboBox1.Items.Add("小清水町")
        ComboBox1.Items.Add("八丈島八丈町")
        ComboBox1.Items.Add("西多摩郡瑞穂町")
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.SelectedIndex = -1
        BindData()
    End Sub
    Private Sub getdata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles getdata.Click
        Dim id As Integer = TextBox1.Text
        Dim query As String = "select * from UserInfo_Table where ID = @id"
        Using con As New SqlConnection("Data Source=LAPTOP-BQ3GKAJC\MSSQLSERVERMMK;Initial Catalog=CRUDTutorial_DB;Integrated Security=True;Pooling=False")
            Using cmd As SqlCommand = New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", id)
                Using da As New SqlDataAdapter()
                    da.SelectCommand = cmd
                    Using dt As New DataTable()
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            TextBox2.Text = dt.Rows(0)(1).ToString()
                            TextBox3.Text = dt.Rows(0)(2).ToString()
                            ComboBox1.Text = dt.Rows(0)(3).ToString()
                            TextBox4.Text = dt.Rows(0)(4).ToString()
                            rdone.Checked = False
                            rdtwo.Checked = False
                            If dt.Rows(0)(5).ToString() = "男" Then
                                rdone.Checked = True
                            End If
                            If dt.Rows(0)(5).ToString() = "女" Then
                                rdtwo.Checked = True
                            End If
                        Else
                            MessageBox.Show("取得対象のデータがありません。")
                            TextBox1.Text = ""
                            TextBox2.Text = ""
                            TextBox3.Text = ""
                            ComboBox1.SelectedIndex = -1
                            TextBox4.Text = ""
                            rdone.Checked = False
                            rdtwo.Checked = False
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub insert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles insert.Click
        Dim id As Integer = TextBox1.Text
        Dim name As String = TextBox2.Text
        Dim address As String = TextBox3.Text
        Dim city As String = ComboBox1.Text
        Dim age As Double = TextBox4.Text
        Dim sex As String = ""
        If rdone.Checked = True Then
            sex = rdone.Text
        Else
            sex = rdtwo.Text
        End If
        Dim query As String = "Insert into UserInfo_Table values (@id, @name, @address, @city, @age, @sex)"
        Using con As New SqlConnection("Data Source=LAPTOP-BQ3GKAJC\MSSQLSERVERMMK;Initial Catalog=CRUDTutorial_DB;Integrated Security=True;Pooling=False")
            Using cmd As SqlCommand = New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.AddWithValue("@name", name)
                cmd.Parameters.AddWithValue("@address", address)
                cmd.Parameters.AddWithValue("@city", city)
                cmd.Parameters.AddWithValue("@age", age)
                cmd.Parameters.AddWithValue("@sex", sex)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                MessageBox.Show("登録しました。")
                BindData()
            End Using
        End Using
    End Sub

    Private Sub update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles update.Click
        Dim id As Integer = TextBox1.Text
        Dim name As String = TextBox2.Text
        Dim address As String = TextBox3.Text
        Dim city As String = ComboBox1.Text
        Dim age As Double = TextBox4.Text
        Dim sex As String = ""
        If rdone.Checked = True Then
            sex = "男"
        Else
            sex = "女"
        End If
        Dim query As String = "Update UserInfo_Table set Name=@name, Address=@address, City=@city, Age=@age, Sex=@sex where ID= @id"
        Using con As New SqlConnection("Data Source=LAPTOP-BQ3GKAJC\MSSQLSERVERMMK;Initial Catalog=CRUDTutorial_DB;Integrated Security=True;Pooling=False")
            Using cmd As SqlCommand = New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.Parameters.AddWithValue("@name", name)
                cmd.Parameters.AddWithValue("@address", address)
                cmd.Parameters.AddWithValue("@city", city)
                cmd.Parameters.AddWithValue("@age", age)
                cmd.Parameters.AddWithValue("@sex", sex)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                MessageBox.Show("修正しました。")
                BindData()
            End Using
        End Using
    End Sub

    Private Sub clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles clear.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.SelectedIndex = -1
        TextBox4.Text = ""
        rdone.Checked = False
        rdtwo.Checked = False
    End Sub
    Private Sub BindData()
        Dim query As String = "select * from UserInfo_Table"
        Using con As New SqlConnection("Data Source=LAPTOP-BQ3GKAJC\MSSQLSERVERMMK;Initial Catalog=CRUDTutorial_DB;Integrated Security=True;Pooling=False")
            Using cmd As SqlCommand = New SqlCommand(query, con)
                Using da As New SqlDataAdapter()
                    da.SelectCommand = cmd
                    Using dt As New DataTable()
                        da.Fill(dt)
                        DataGridView1.DataSource = dt
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        Dim id As Integer = TextBox1.Text
        Dim query As String = "Delete UserInfo_Table where ID= @id"
        Using con As New SqlConnection("Data Source=LAPTOP-BQ3GKAJC\MSSQLSERVERMMK;Initial Catalog=CRUDTutorial_DB;Integrated Security=True;Pooling=False")
            Using cmd As SqlCommand = New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@id", id)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                MessageBox.Show("削除しました。")
                BindData()
            End Using
        End Using
    End Sub
End Class
