Public Class PictureViewer
    Private Sub PictureViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ImageBox.BackgroundImage = Main.PreviewImage.BackgroundImage()
        Me.Height = Main.PicHight
        Me.Width = Main.PicWidth
    End Sub

    Private Sub PictureViewer_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Dim Height As Integer = Me.Size.Height
        Dim Width As Integer = Me.Size.Width
        Me.ImageBox.Height = Height - 42
        Me.ImageBox.Width = Width - 20
    End Sub
End Class