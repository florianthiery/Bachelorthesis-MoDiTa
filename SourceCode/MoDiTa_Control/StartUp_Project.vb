Public Class StartUp_Project
    Public message As String
    Public lastproject As Boolean = True

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        message = "last"
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        message = "open"
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        message = "new"
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        message = "cancel"
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub StartUp_Project_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (lastproject = True) Then
            La_Text.Text = "Open last project, an another project or a new project?"
            Button1.Visible = True
            Button2.Visible = True
            Button3.Visible = True
            Button4.Visible = True
        Else
            La_Text.Text = "Open project or new project?"
            Button1.Visible = False
            Button2.Visible = True
            Button3.Visible = True
            Button4.Visible = True
        End If
    End Sub
End Class