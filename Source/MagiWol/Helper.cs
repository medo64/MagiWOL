using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MagiWol {
    internal static class Helper {

        #region ToolStripBorderlessProfessionalRenderer

        internal class ToolStripBorderlessProfessionalRenderer : ToolStripProfessionalRenderer {

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
            }

        }

        #endregion

        #region Toolstrip images

        internal static void UpdateToolstripImages(params ToolStrip[] toolstrips) {
            if (toolstrips == null) { return; }

            var form = toolstrips[0].Parent as Form;

            using (var g = form.CreateGraphics()) {
                var scale = Math.Max(Math.Max(g.DpiX, g.DpiY) / 96.0, 1);

                int size;
                string set;
                if (scale < 1.5) {
                    size = 16;
                    set = "_16";
                } else if (scale < 2) {
                    size = 24;
                    set = "_24";
                } else if (scale < 3) {
                    size = 32;
                    set = "_32";
                } else {
                    var base32 = 16 * scale / 32;
                    var base48 = 16 * scale / 48;
                    if ((base48 - (int)base48) < (base32 - (int)base32)) {
                        size = 48 * (int)base48;
                        set = "_48";
                    } else {
                        size = 32 * (int)base32;
                        set = "_32";
                    }
                }

                var resources = MagiWol.Properties.Resources.ResourceManager;

                foreach (var toolstrip in toolstrips) {
                    toolstrip.ImageScalingSize = new Size(size, size);

                    foreach (ToolStripItem item in toolstrip.Items) {
                        Bitmap bitmap = null;
                        if (!string.IsNullOrEmpty(item.Name)) {
                            bitmap = resources.GetObject(item.Name + set) as Bitmap;
                        }
                        if ((bitmap == null) && !string.IsNullOrEmpty(item.Tag as string)) {
                            bitmap = resources.GetObject(item.Tag + set) as Bitmap;
                        }

                        item.ImageScaling = ToolStripItemImageScaling.None;
#if DEBUG
                        item.Image = (bitmap != null) ? new Bitmap(bitmap, size, size) : new Bitmap(size, size, PixelFormat.Format8bppIndexed);
#else
                        if (bitmap != null) { item.Image = new Bitmap(bitmap, size, size); }
#endif
                    }
                }
            }
        }

        #endregion

    }
}
