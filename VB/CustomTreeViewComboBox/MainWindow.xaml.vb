Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports System.Collections
Imports System.Collections.ObjectModel

Namespace CustomTreeViewComboBox
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()
            AddHandler Me.Loaded, AddressOf MainWindow_Loaded
        End Sub

        Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.DataContext = Me
            lookUpEdit.ItemsSource = Stuff.GetStuff()
        End Sub
    End Class

    Public Class Employee
        Public Property ID() As Integer
        Public Property ParentID() As Integer
        Public Property Name() As String
        Public Property Position() As String
        Public Property Department() As String
    End Class

    Public NotInheritable Class Stuff

        Private Sub New()
        End Sub

        Public Shared Function GetStuff() As List(Of Employee)

            Dim stuff_Renamed As New List(Of Employee)()
            stuff_Renamed.Add(New Employee() With { _
                .ID = 1, _
                .ParentID = 0, _
                .Name = "Gregory S. Price", _
                .Department = "", _
                .Position = "President" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 2, _
                .ParentID = 1, _
                .Name = "Irma R. Marshall", _
                .Department = "Marketing", _
                .Position = "Vice President" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 3, _
                .ParentID = 1, _
                .Name = "John C. Powell", _
                .Department = "Operations", _
                .Position = "Vice President" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 4, _
                .ParentID = 1, _
                .Name = "Christian P. Laclair", _
                .Department = "Production", _
                .Position = "Vice President" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 5, _
                .ParentID = 1, _
                .Name = "Karen J. Kelly", _
                .Department = "Finance", _
                .Position = "Vice President" _
            })

            stuff_Renamed.Add(New Employee() With { _
                .ID = 6, _
                .ParentID = 2, _
                .Name = "Brian C. Cowling", _
                .Department = "Marketing", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 7, _
                .ParentID = 2, _
                .Name = "Thomas C. Dawson", _
                .Department = "Marketing", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 8, _
                .ParentID = 2, _
                .Name = "Angel M. Wilson", _
                .Department = "Marketing", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 9, _
                .ParentID = 2, _
                .Name = "Bryan R. Henderson", _
                .Department = "Marketing", _
                .Position = "Manager" _
            })

            stuff_Renamed.Add(New Employee() With { _
                .ID = 10, _
                .ParentID = 3, _
                .Name = "Harold S. Brandes", _
                .Department = "Operations", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 11, _
                .ParentID = 3, _
                .Name = "Michael S. Blevins", _
                .Department = "Operations", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 12, _
                .ParentID = 3, _
                .Name = "Jan K. Sisk", _
                .Department = "Operations", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 13, _
                .ParentID = 3, _
                .Name = "Sidney L. Holder", _
                .Department = "Operations", _
                .Position = "Manager" _
            })

            stuff_Renamed.Add(New Employee() With { _
                .ID = 14, _
                .ParentID = 4, _
                .Name = "James L. Kelsey", _
                .Department = "Production", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 15, _
                .ParentID = 4, _
                .Name = "Howard M. Carpenter", _
                .Department = "Production", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 16, _
                .ParentID = 4, _
                .Name = "Jennifer T. Tapia", _
                .Department = "Production", _
                .Position = "Manager" _
            })

            stuff_Renamed.Add(New Employee() With { _
                .ID = 17, _
                .ParentID = 5, _
                .Name = "Judith P. Underhill", _
                .Department = "Finance", _
                .Position = "Manager" _
            })
            stuff_Renamed.Add(New Employee() With { _
                .ID = 18, _
                .ParentID = 5, _
                .Name = "Russell E. Belton", _
                .Department = "Finance", _
                .Position = "Manager" _
            })
            Return stuff_Renamed
        End Function
    End Class
End Namespace
