using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GAFarm.Common.Interface;

namespace GAFarm.Common.Define
{
    public class ScanResult
    {
        private GeoInfo geoInfo;

        private ICreature targetCreature;

        private bool isFind = false;

        public ScanResult(bool isFind, ICreature target)
        {
            geoInfo = new GeoInfo(new double[2]);
            geoInfo.DimensionInfo[0] = target.CurrentX;
            geoInfo.DimensionInfo[1] = target.CurrentY;
            this.isFind = isFind;
            this.targetCreature = target;
        }

        public double X
        {
            get
            {
                return geoInfo.DimensionInfo[0];
            }
        }

        public double Y
        {
            get
            {
                return geoInfo.DimensionInfo[1];
            }
        }

        public GeoInfo ScanGeoInfo
        {
            get
            {
                return geoInfo;
            }
        }

        public ICreature TargetCreature
        {
            get
            {
                return targetCreature;
            }
        }

        public bool IsFind
        {
            get
            {
                return this.isFind;
            }
        }
    }

    public class MoveResult
    {
        private GeoInfo geoInfo;

        public MoveResult(GeoInfo geoInfoInput)
        {
            geoInfo = geoInfoInput;
        }

        public double X
        {
            get
            {
                return geoInfo.DimensionInfo[0];
            }
        }

        public double Y
        {
            get
            {
                return geoInfo.DimensionInfo[1];
            }
        }

        public GeoInfo MoveGeoInfo
        {
            get
            {
                return geoInfo;
            }
        }
    }
}
