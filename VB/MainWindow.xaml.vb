Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.Helpers
Imports DevExpress.Xpf.Editors.Internal
Imports DevExpress.Xpf.Editors.Native
Imports DevExpress.Xpf.Editors.Settings

Namespace CustomTreeViewComboBox
	Partial Public Class MainWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = DataSource.GetData()
		End Sub
	End Class

	Public NotInheritable Class DataSource
		Private Sub New()
		End Sub
		Public Shared Function GetData() As IEnumerable(Of TestData)
			Return New List(Of TestData) (New TestData() {New TestData() With {.Item1 = 1, .Item2 = "Test1"}, New TestData() With {.Item1 = 2, .Item2 = "Test2"}, New TestData() With {.Item1 = 3, .Item2 = "Test3"}, New TestData() With {.Item1 = 4, .Item2 = "Test4"}, New TestData() With {.Item1 = 5, .Item2 = "Test5"}})
		End Function
		Public Shared ReadOnly Property Data() As IEnumerable(Of TestData)
			Get
				Return GetData()
			End Get
		End Property
	End Class

	Public Class TestData
		Private privateItem1 As Integer
		Public Property Item1() As Integer
			Get
				Return privateItem1
			End Get
			Set(ByVal value As Integer)
				privateItem1 = value
			End Set
		End Property
		Private privateItem2 As String
		Public Property Item2() As String
			Get
				Return privateItem2
			End Get
			Set(ByVal value As String)
				privateItem2 = value
			End Set
		End Property
	End Class

	Public Class TreeViewComboBoxEdit
		Inherits ComboBoxEdit
		Shared Sub New()
			Dim ownerType As Type = GetType(TreeViewComboBoxEdit)
			DefaultStyleKeyProperty.OverrideMetadata(ownerType, New FrameworkPropertyMetadata(ownerType))

			EditorSettingsProvider.Default.RegisterUserEditor(GetType(TreeViewComboBoxEdit), GetType(TreeViewComboBoxEditSettings), Function() New TreeViewComboBoxEdit(), Function() New TreeViewComboBoxEditSettings())
		End Sub
		Public Sub New()
		End Sub

		Protected Overrides Function CreateEditStrategy() As EditStrategyBase
			Return New TreeViewComboBoxEditStrategy(Me)
		End Function
	End Class

	Public Class TreeViewComboBoxEditSettings
		Inherits ComboBoxEditSettings
		Shared Sub New()
			EditorSettingsProvider.Default.RegisterUserEditor(GetType(TreeViewComboBoxEdit), GetType(TreeViewComboBoxEditSettings), Function() New TreeViewComboBoxEdit(), Function() New TreeViewComboBoxEditSettings())
		End Sub
	End Class

	Public Class TreeViewComboBoxEditStrategy
		Inherits ComboBoxEditStrategy
		Public Sub New(ByVal editor As ComboBoxEdit)
			MyBase.New(editor)
		End Sub

		Protected Overrides Function CreateVisualClient() As VisualClientOwner
			Return New TreeViewVisualClientOwner(CType(Editor, TreeViewComboBoxEdit))
		End Function
	End Class

	Public Class TreeViewVisualClientOwner
		Inherits VisualClientOwner
		Public Sub New(ByVal editor As ComboBoxEdit)
			MyBase.New(editor)
		End Sub

		Private Shadows ReadOnly Property Editor() As ComboBoxEdit
			Get
				Return TryCast(MyBase.Editor, ComboBoxEdit)
			End Get
		End Property
		Private ReadOnly Property TreeView() As TreeView
			Get
				Return TryCast(InnerEditor, TreeView)
			End Get
		End Property

		Protected Overrides Function FindEditor() As FrameworkElement
			If LookUpEditHelper.GetPopupContentOwner(Editor).Child Is Nothing Then
				Return Nothing
			End If
			Return LayoutHelper.FindElementByName(LookUpEditHelper.GetPopupContentOwner(Editor).Child, "PART_Content")
		End Function
		Public Overrides Function GetSelectedItem() As Object
			If (Not IsLoaded) Then
				Return Nothing
			End If
			Return TreeView.SelectedItem
		End Function
		Public Overrides Function GetSelectedItems() As IEnumerable
			If (Not IsLoaded) Then
				Return Nothing
			End If
			Return New List(Of Object) (New Object() {TreeView.SelectedItem})
		End Function
		Protected Overrides Sub SetupEditor()
			If (Not IsLoaded) Then
				Return
			End If
			SyncProperties(True)
		End Sub
		Public Overrides Sub SyncProperties(ByVal syncDataSource As Boolean)
			If (Not IsLoaded) Then
				Return
			End If
			If syncDataSource Then
				TreeView.ItemsSource = (CType(Editor, ISelectorEdit)).GetPopupContentItemsSource()
			End If
			TreeView.SelectedValuePath = Editor.ValueMember
			TreeView.DisplayMemberPath = Editor.DisplayMember
		End Sub
		Public Overrides Sub PopupOpened()
			MyBase.PopupOpened()
			AddHandler TreeView.SelectedItemChanged, AddressOf TreeView_SelectedItemChanged
		End Sub
		Public Overrides Sub PopupClosed()
			RemoveHandler TreeView.SelectedItemChanged, AddressOf TreeView_SelectedItemChanged
			MyBase.PopupClosed()
		End Sub
		Private Sub TreeView_SelectedItemChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Object))
			Editor.ClosePopup()
		End Sub
		Public Overrides Sub ProcessKeyDownInternal(ByVal e As KeyEventArgs)
			Throw New NotImplementedException()
		End Sub
	End Class
End Namespace
