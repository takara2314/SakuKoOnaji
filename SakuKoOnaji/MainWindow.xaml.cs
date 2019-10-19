using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace SakuKoOnaji {
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		string FileInfoStereotypes = "変換完了：";

		private void DDbox_PreviewDragOver(object sender, DragEventArgs e) {
			e.Effects = DragDropEffects.Copy;
			e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
		}

		async private void DDbox_Drop(object sender, DragEventArgs e) {
			// ドロップされたものが複数な場合は、各ファイルのパス文字列を文字列配列に格納する
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			if (files != null) {
				foreach (string uriString in files) {
					// 絶対パス取得
					string FullPath = System.IO.Path.GetFullPath(uriString);

					// ファイル情報を取得
					FileInfo fi = new FileInfo(FullPath);
					// 作成日時と更新日時を同じにする
					fi.CreationTime = fi.LastWriteTime;

					// 絶対パスを取得してテキストブロックに書き込む
					FileInfoBlock.Text = FileInfoStereotypes + FullPath;
					if (files.Length > 1)
						// 非同期に処理を止める
						await Task.Delay(50);
				}
				MessageBox.Show("すべて完了しました。");
			}
		}
	}
}
