<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtDatos = New System.Windows.Forms.TextBox()
        Me.btnFtpUp = New System.Windows.Forms.Button()
        Me.btnListar = New System.Windows.Forms.Button()
        Me.btnBorrar = New System.Windows.Forms.Button()
        Me.btnExisteDir = New System.Windows.Forms.Button()
        Me.btnImagen = New System.Windows.Forms.Button()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.btnParalle_Invoke = New System.Windows.Forms.Button()
        Me.btnIP = New System.Windows.Forms.Button()
        Me.btnCollections = New System.Windows.Forms.Button()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDatos
        '
        Me.txtDatos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDatos.Location = New System.Drawing.Point(12, 149)
        Me.txtDatos.Multiline = True
        Me.txtDatos.Name = "txtDatos"
        Me.txtDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDatos.Size = New System.Drawing.Size(587, 302)
        Me.txtDatos.TabIndex = 1
        '
        'btnFtpUp
        '
        Me.btnFtpUp.Location = New System.Drawing.Point(222, 12)
        Me.btnFtpUp.Name = "btnFtpUp"
        Me.btnFtpUp.Size = New System.Drawing.Size(168, 39)
        Me.btnFtpUp.TabIndex = 2
        Me.btnFtpUp.Text = "FTP Subir Fichero"
        Me.btnFtpUp.UseVisualStyleBackColor = True
        '
        'btnListar
        '
        Me.btnListar.Location = New System.Drawing.Point(424, 12)
        Me.btnListar.Name = "btnListar"
        Me.btnListar.Size = New System.Drawing.Size(175, 39)
        Me.btnListar.TabIndex = 3
        Me.btnListar.Text = "Listar Ficheros"
        Me.btnListar.UseVisualStyleBackColor = True
        '
        'btnBorrar
        '
        Me.btnBorrar.Location = New System.Drawing.Point(222, 77)
        Me.btnBorrar.Name = "btnBorrar"
        Me.btnBorrar.Size = New System.Drawing.Size(168, 44)
        Me.btnBorrar.TabIndex = 4
        Me.btnBorrar.Text = "Borrar Ficheros"
        Me.btnBorrar.UseVisualStyleBackColor = True
        '
        'btnExisteDir
        '
        Me.btnExisteDir.Location = New System.Drawing.Point(21, 12)
        Me.btnExisteDir.Name = "btnExisteDir"
        Me.btnExisteDir.Size = New System.Drawing.Size(132, 39)
        Me.btnExisteDir.TabIndex = 5
        Me.btnExisteDir.Text = "Existe Dir"
        Me.btnExisteDir.UseVisualStyleBackColor = True
        '
        'btnImagen
        '
        Me.btnImagen.Location = New System.Drawing.Point(21, 77)
        Me.btnImagen.Name = "btnImagen"
        Me.btnImagen.Size = New System.Drawing.Size(132, 33)
        Me.btnImagen.TabIndex = 6
        Me.btnImagen.Text = "Descarga Imagen"
        Me.btnImagen.UseVisualStyleBackColor = True
        '
        'pb1
        '
        Me.pb1.Location = New System.Drawing.Point(625, 149)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(318, 302)
        Me.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pb1.TabIndex = 7
        Me.pb1.TabStop = False
        '
        'btnParalle_Invoke
        '
        Me.btnParalle_Invoke.Location = New System.Drawing.Point(648, 19)
        Me.btnParalle_Invoke.Name = "btnParalle_Invoke"
        Me.btnParalle_Invoke.Size = New System.Drawing.Size(241, 32)
        Me.btnParalle_Invoke.TabIndex = 8
        Me.btnParalle_Invoke.Text = "Parallel Invoke"
        Me.btnParalle_Invoke.UseVisualStyleBackColor = True
        '
        'btnIP
        '
        Me.btnIP.Location = New System.Drawing.Point(424, 80)
        Me.btnIP.Name = "btnIP"
        Me.btnIP.Size = New System.Drawing.Size(144, 39)
        Me.btnIP.TabIndex = 9
        Me.btnIP.Text = "Pon IP's"
        Me.btnIP.UseVisualStyleBackColor = True
        '
        'btnCollections
        '
        Me.btnCollections.Location = New System.Drawing.Point(733, 64)
        Me.btnCollections.Name = "btnCollections"
        Me.btnCollections.Size = New System.Drawing.Size(156, 28)
        Me.btnCollections.TabIndex = 10
        Me.btnCollections.Text = "MKKIT_Collections"
        Me.btnCollections.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 463)
        Me.Controls.Add(Me.btnCollections)
        Me.Controls.Add(Me.btnIP)
        Me.Controls.Add(Me.btnParalle_Invoke)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.btnImagen)
        Me.Controls.Add(Me.btnExisteDir)
        Me.Controls.Add(Me.btnBorrar)
        Me.Controls.Add(Me.btnListar)
        Me.Controls.Add(Me.btnFtpUp)
        Me.Controls.Add(Me.txtDatos)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDatos As TextBox
    Friend WithEvents btnFtpUp As Button
    Friend WithEvents btnListar As Button
    Friend WithEvents btnBorrar As Button
    Friend WithEvents btnExisteDir As Button
    Friend WithEvents btnImagen As Button
    Friend WithEvents pb1 As PictureBox
    Friend WithEvents btnParalle_Invoke As Button
    Friend WithEvents btnIP As Button
    Friend WithEvents btnCollections As Button
End Class
