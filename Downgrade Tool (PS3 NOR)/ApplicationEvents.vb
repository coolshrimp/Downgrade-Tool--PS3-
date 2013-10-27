Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Sub AppStartup() Handles Me.Startup
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf CurrentDomainAssemblyResolve
        End Sub
        Function CurrentDomainAssemblyResolve(ByVal sender As Object, ByVal e As ResolveEventArgs) As Reflection.Assembly
            If String.IsNullOrEmpty(e.Name) Then
                Throw New Exception("DLL Read Failure (Nothing to load!)")
            End If
            Dim name As String = String.Format("{0}.dll", e.Name.Split(","c)(0))
            Using stream = Reflection.Assembly.GetAssembly(GetType(Main)).GetManifestResourceStream(String.Format("{0}.{1}", GetType(Main).Namespace, name))
                If stream IsNot Nothing Then
                    Dim data = New Byte(stream.Length - 1) {}
                    stream.Read(data, 0, data.Length)
                    Return Reflection.Assembly.Load(data)
                End If
                Throw New Exception(String.Format("Can't find external nor internal {0}!", name))
            End Using
        End Function
    End Class
End Namespace

