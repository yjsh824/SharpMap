using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using SharpMap.Layers;
using BruTile.Predefined;

using WinFormSamples.Properties;

using GeometryTransform = GeoAPI.CoordinateSystems.Transformations.GeometryTransform;
using BruTile.Samples.Common;
using System.Drawing;

namespace WinFormSamples
{
    public partial class FormMovingObjectOverTileLayer : Form
    {

        private List<IGeometry> geos = new List<IGeometry>();

        private bool movingUp = true;
        private bool movingLeft = true;
        GeoAPI.Geometries.Coordinate position;
        VectorLayer pushPinLayer;
        public FormMovingObjectOverTileLayer()
        {
   
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }

        private void FormMovingObjectOverTileLayer_Load(object sender, EventArgs e)
        {

            //Lisbon...
            //var mathTransform = LayerTools.Wgs84toGoogleMercator.MathTransform;
            //GeoAPI.Geometries.Envelope geom = GeometryTransform.TransformBox(
            //    new Envelope(114.205626, 115.123736, 36.690993, 38.740837),
            //    mathTransform);
            GeoAPI.Geometries.Envelope geom=new GeoAPI.Geometries.Envelope(134.6, 137.0, 33.68, 35.12);
            //Google Background
            //TileAsyncLayer layer2 = new TileAsyncLayer(KnownTileSources.Create(KnownTileSource.OpenStreetMap), "TileLayer - OSM");
            TileAsyncLayer tiandituLayer = new TileAsyncLayer(LantMaterietTopowebbTileSourceTest.Create(), "tianditu");

            this.mapBox1.Map.BackgroundLayer.Add(tiandituLayer);
            //var gf = new GeometryFactory(new PrecisionModel(), 4490);

            ////Adds a static layer
            //var staticLayer = new VectorLayer("Fixed Marker");
            ////position = geom.GetCentroid();
            //var aux = new List<IGeometry>();
            //aux.Add(gf.CreatePoint(geom.Centre));
            //staticLayer.Style.Symbol = Resources.PumpSmall;
            //var geoProviderFixed = new SharpMap.Data.Providers.GeometryProvider(aux);
            //staticLayer.DataSource = geoProviderFixed;
            //this.mapBox1.Map.Layers.Add(staticLayer);

            
            //Adds a moving variable layer
            //pushPinLayer = new VectorLayer("PushPins");
            //position = new GeoAPI.Geometries.Coordinate(134.77, 33.77);
            //geos.Add(gf.CreatePoint(position));
            //pushPinLayer.Style.Symbol = Resources.car;
            //var geoProvider = new SharpMap.Data.Providers.GeometryProvider(geos);
            //pushPinLayer.DataSource = geoProvider;
            //this.mapBox1.Map.VariableLayers.Add(pushPinLayer);

            //划线
           
            //var lineLayer = new VectorLayer("line");
            //var lines = new List<IGeometry>();
            //var l = gf.CreateLineString(
            // new[]
            //     {   new GeoAPI.Geometries.Coordinate(134.77, 33.77),
            //            new GeoAPI.Geometries.Coordinate(134.82, 35.10), new GeoAPI.Geometries.Coordinate(135.82, 34.90)});
            //lines.Add(l);
            //lineLayer.Style.Line= new Pen(Color.Black, 3);
            //var geoLineProvider = new SharpMap.Data.Providers.GeometryProvider(lines);
            //lineLayer.DataSource = geoLineProvider;
            //this.mapBox1.Map.VariableLayers.Add(lineLayer);

            this.mapBox1.Map.ZoomToBox(geom);
            this.mapBox1.Refresh();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double dx, dy;
            if (movingLeft)
                dx = -0.01;
            else
                dx = 0.01;

            if (movingUp)
                dy = 0.01;
            else
                dy = -0.01;

            position.X = position.X + dx;
            position.Y = position.Y + dy;
           
            if (position.X < this.mapBox1.Map.Envelope.MinX)
                movingLeft = false;
            else if (position.X > this.mapBox1.Map.Envelope.MaxX)
                movingLeft = true;

            if (position.Y < this.mapBox1.Map.Envelope.MinY)
                movingUp = true;
            else if (position.Y > this.mapBox1.Map.Envelope.MaxY)
                movingUp = false;
            pushPinLayer.Style.SymbolRotation = 33.2222f;
            VariableLayerCollection.TouchTimer();
            //this.mapBox1.Refresh();

        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
        }


        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            this.mapBox1.Refresh();
        }

        private void FormMovingObjectOverTileLayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }


    }
}
