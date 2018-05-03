﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItemValidity
    {
        private bool barcodeIsValid = false;
        private bool nameIsValid = false;
        private bool descriptionIsValid = false;
        private bool priceIsValid = false;
        private bool categoryIsValid = false;
        private bool modelIsValid = false;
        private bool numberAvailableIsValid = false;

        public bool BarcodeIsValid
        {
            get { return barcodeIsValid; }
            set { barcodeIsValid = value; }
        }

        public bool NameIsValid
        {
            get { return nameIsValid; }
            set { nameIsValid = value; }
        }

        public bool DescriptionIsValid
        {
            get { return descriptionIsValid; }
            set { descriptionIsValid = value; }
        }

        public bool PriceIsValid
        {
            get { return priceIsValid; }
            set { priceIsValid = value; }
        }

        public bool CategoryIsValid
        {
            get { return categoryIsValid; }
            set { categoryIsValid = value; }
        }

        public bool ModelIsValid
        {
            get { return modelIsValid; }
            set { modelIsValid = value; }
        }

        public bool NumberAvailableIsValid
        {
            get { return numberAvailableIsValid; }
            set { numberAvailableIsValid = value; }
        }
    }
}