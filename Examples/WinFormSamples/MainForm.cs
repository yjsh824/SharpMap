using BruTile.Samples.Common;
using SharpMap;
using SharpMap.Layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormSamples
{
    public partial class MainForm : Form
    {
        private static readonly Dictionary<string, Type> MapDecorationTypes = new Dictionary<string, Type>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            ShowMap(MapType.RunLine);
        }

        private void btnDo2_Click(object sender, EventArgs e)
        {
            ShowMap(MapType.Static);
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "png";
            sfd.Filter = "PNG图片|*.png|JPG图片|*.jpg";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                Image image = this.mapBox1.Map.GetMap();
                image.Save(fileName);
            }
        }

        private void btnMapInfo_Click(object sender, EventArgs e)
        {
            ShowMap(MapType.Tianditu);
        }

        private void btnShapFile_Click(object sender, EventArgs e)
        {
            ShowMap(MapType.ShapeFile);
        }

        private void ShowMap(MapType tt)
        {
            Cursor mic = mapBox1.Cursor;
            mapBox1.Cursor = Cursors.WaitCursor;
            Cursor = Cursors.WaitCursor;
            try
            {
                mapBox1.Map = SharpMapHelper.InitializeMap(tt, 0);
                mapBox1.Map.Size = Size;
                //if(tt==MapType.Tianditu)
                //{
                //    GeoAPI.Geometries.Envelope geom = new GeoAPI.Geometries.Envelope(132.6, 137.0, 31.68, 35.12);
                //    this.mapBox1.Map.ZoomToBox(geom);
                //}
                mapBox1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            Cursor = Cursors.Default;
            mapBox1.Cursor = mic;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //GeoAPI.Geometries.Envelope geom = new GeoAPI.Geometries.Envelope(134.6, 137.0, 33.68, 35.12);
            //TileAsyncLayer tiandituLayer = new TileAsyncLayer(TDTTrans.Create(), "tianditu");

            //this.mapBox1.Map.BackgroundLayer.Add(tiandituLayer);
            //this.mapBox1.Map.ZoomToBox(geom);
            //this.mapBox1.Refresh();
        }

        private void btnTDT_Click(object sender, EventArgs e)
        {
            //GeoAPI.Geometries.Envelope geom = new GeoAPI.Geometries.Envelope(134.6, 137.0, 33.68, 35.12);
            //TileAsyncLayer tiandituLayer = new TileAsyncLayer(TDTTrans.Create(), "tianditu");
            //var map = new Map();
            //map.BackgroundLayer.Add(tiandituLayer);
            //this.mapBox1.Map = map;
            ////this.mapBox1.Map.BackgroundLayer.Add(tiandituLayer);
            //this.mapBox1.Map.ZoomToBox(geom);
            //this.mapBox1.Refresh();
            ShowMap(MapType.Tianditu);
        }
    }
}
