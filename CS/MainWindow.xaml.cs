using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Internal;
using DevExpress.Xpf.Editors.Native;
using DevExpress.Xpf.Editors.Settings;

namespace CustomTreeViewComboBox {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            grid.ItemsSource = DataSource.GetData();
        }
    }

    public static class DataSource {
        public static IEnumerable<TestData> GetData() {
            return new List<TestData> {
                new TestData() { Item1 = 1, Item2 = "Test1" },
                new TestData() { Item1 = 2, Item2 = "Test2" },
                new TestData() { Item1 = 3, Item2 = "Test3" },
                new TestData() { Item1 = 4, Item2 = "Test4" },
                new TestData() { Item1 = 5, Item2 = "Test5" },
            };
        }
        public static IEnumerable<TestData> Data { get { return GetData(); } }
    }

    public class TestData {
        public int Item1 { get; set; }
        public string Item2 { get; set; }
    }

    public class TreeViewComboBoxEdit : ComboBoxEdit {
        static TreeViewComboBoxEdit() {
            Type ownerType = typeof(TreeViewComboBoxEdit);
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));

            EditorSettingsProvider.Default.RegisterUserEditor(
                typeof(TreeViewComboBoxEdit),
                typeof(TreeViewComboBoxEditSettings),
                () => new TreeViewComboBoxEdit(),
                () => new TreeViewComboBoxEditSettings());
        }
        public TreeViewComboBoxEdit() { }

        protected override EditStrategyBase CreateEditStrategy() {
            return new TreeViewComboBoxEditStrategy(this);
        }
    }

    public class TreeViewComboBoxEditSettings : ComboBoxEditSettings {
        static TreeViewComboBoxEditSettings() {
            EditorSettingsProvider.Default.RegisterUserEditor(
                typeof(TreeViewComboBoxEdit),
                typeof(TreeViewComboBoxEditSettings),
                () => new TreeViewComboBoxEdit(),
                () => new TreeViewComboBoxEditSettings());
        }
    }

    public class TreeViewComboBoxEditStrategy : ComboBoxEditStrategy {
        public TreeViewComboBoxEditStrategy(ComboBoxEdit editor) : base(editor) { }

        protected override VisualClientOwner CreateVisualClient() {
            return new TreeViewVisualClientOwner((TreeViewComboBoxEdit)Editor);
        }
    }

    public class TreeViewVisualClientOwner : VisualClientOwner {
        public TreeViewVisualClientOwner(ComboBoxEdit editor) : base(editor) { }

        new ComboBoxEdit Editor { get { return base.Editor as ComboBoxEdit; } }
        TreeView TreeView { get { return InnerEditor as TreeView; } }

        protected override FrameworkElement FindEditor() {
            if(LookUpEditHelper.GetPopupContentOwner(Editor).Child == null)
                return null;
            return LayoutHelper.FindElementByName(LookUpEditHelper.GetPopupContentOwner(Editor).Child, "PART_Content");
        }
        public override object GetSelectedItem() {
            if(!IsLoaded)
                return null;
            return TreeView.SelectedItem;
        }
        public override IEnumerable GetSelectedItems() {
            if(!IsLoaded)
                return null;
            return new List<object> { TreeView.SelectedItem };
        }
        protected override void SetupEditor() {
            if(!IsLoaded)
                return;
            SyncProperties(true);
        }
        public override void SyncProperties(bool syncDataSource) {
            if(!IsLoaded)
                return;
            if(syncDataSource)
                TreeView.ItemsSource = ((ISelectorEdit)Editor).GetPopupContentItemsSource();
            TreeView.SelectedValuePath = Editor.ValueMember;
            TreeView.DisplayMemberPath = Editor.DisplayMember;
        }
        public override void PopupOpened() {
            base.PopupOpened();
            TreeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(TreeView_SelectedItemChanged);
        }
        public override void PopupClosed() {
            TreeView.SelectedItemChanged -= new RoutedPropertyChangedEventHandler<object>(TreeView_SelectedItemChanged);
            base.PopupClosed();
        }
        void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            Editor.ClosePopup();
        }
        public override void ProcessKeyDown(KeyEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
