﻿/*
 Highcharts JS v6.0.1 (2017-10-05)
 Exporting module

 (c) 2010-2017 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (k) { "object" === typeof module && module.exports ? module.exports = k : k(Highcharts) })(function (k) {
    (function (f) {
        var k = f.defaultOptions, z = f.doc, B = f.Chart, w = f.addEvent, I = f.removeEvent, F = f.fireEvent, p = f.createElement, C = f.discardElement, u = f.css, n = f.merge, D = f.pick, h = f.each, G = f.objectEach, r = f.extend, J = f.isTouchDevice, E = f.win, H = E.navigator.userAgent, K = f.Renderer.prototype.symbols; /Edge\/|Trident\/|MSIE /.test(H); /firefox/i.test(H); r(k.lang, {
            printChart: "Print chart", downloadPNG: "Download PNG image", downloadJPEG: "Download JPEG image",
            downloadPDF: "Download PDF document", downloadSVG: "Download SVG vector image", contextButtonTitle: "Chart context menu"
        }); k.navigation = { buttonOptions: { theme: {}, symbolSize: 14, symbolX: 12.5, symbolY: 10.5, align: "right", buttonSpacing: 3, height: 22, verticalAlign: "top", width: 24 } }; n(!0, k.navigation, {
            menuStyle: { border: "1px solid #999999", background: "#ffffff", padding: "5px 0" }, menuItemStyle: { padding: "0.5em 1em", background: "none", color: "#333333", fontSize: J ? "14px" : "11px", transition: "background 250ms, color 250ms" }, menuItemHoverStyle: {
                background: "#335cad",
                color: "#ffffff"
            }, buttonOptions: { symbolFill: "#666666", symbolStroke: "#666666", symbolStrokeWidth: 3, theme: { fill: "#ffffff", stroke: "none", padding: 5 } }
        }); k.exporting = {
            type: "image/png", url: "https://export.highcharts.com/", printMaxWidth: 780, scale: 2, buttons: { contextButton: { className: "highcharts-contextbutton", menuClassName: "highcharts-contextmenu", symbol: "menu", _titleKey: "contextButtonTitle", menuItems: "printChart separator downloadPNG downloadJPEG downloadPDF downloadSVG".split(" ") } }, menuItemDefinitions: {
                printChart: {
                    textKey: "printChart",
                    onclick: function () { this.print() }
                }, separator: { separator: !0 }, downloadPNG: { textKey: "downloadPNG", onclick: function () { this.exportChart() } }, downloadJPEG: { textKey: "downloadJPEG", onclick: function () { this.exportChart({ type: "image/jpeg" }) } }, downloadPDF: { textKey: "downloadPDF", onclick: function () { this.exportChart({ type: "application/pdf" }) } }, downloadSVG: { textKey: "downloadSVG", onclick: function () { this.exportChart({ type: "image/svg+xml" }) } }
            }
        }; f.post = function (a, b, e) {
            var c = p("form", n({ method: "post", action: a, enctype: "multipart/form-data" },
            e), { display: "none" }, z.body); G(b, function (a, b) { p("input", { type: "hidden", name: b, value: a }, null, c) }); c.submit(); C(c)
        }; r(B.prototype, {
            sanitizeSVG: function (a, b) {
                if (b && b.exporting && b.exporting.allowHTML) { var e = a.match(/<\/svg>(.*?$)/); e && e[1] && (e = '\x3cforeignObject x\x3d"0" y\x3d"0" width\x3d"' + b.chart.width + '" height\x3d"' + b.chart.height + '"\x3e\x3cbody xmlns\x3d"http://www.w3.org/1999/xhtml"\x3e' + e[1] + "\x3c/body\x3e\x3c/foreignObject\x3e", a = a.replace("\x3c/svg\x3e", e + "\x3c/svg\x3e")) } a = a.replace(/zIndex="[^"]+"/g,
                "").replace(/isShadow="[^"]+"/g, "").replace(/symbolName="[^"]+"/g, "").replace(/jQuery[0-9]+="[^"]+"/g, "").replace(/url\(("|&quot;)(\S+)("|&quot;)\)/g, "url($2)").replace(/url\([^#]+#/g, "url(#").replace(/<svg /, '\x3csvg xmlns:xlink\x3d"http://www.w3.org/1999/xlink" ').replace(/ (NS[0-9]+\:)?href=/g, " xlink:href\x3d").replace(/\n/, " ").replace(/<\/svg>.*?$/, "\x3c/svg\x3e").replace(/(fill|stroke)="rgba\(([ 0-9]+,[ 0-9]+,[ 0-9]+),([ 0-9\.]+)\)"/g, '$1\x3d"rgb($2)" $1-opacity\x3d"$3"').replace(/&nbsp;/g,
                "\u00a0").replace(/&shy;/g, "\u00ad"); this.ieSanitizeSVG && (a = this.ieSanitizeSVG(a)); return a
            }, getChartHTML: function () { return this.container.innerHTML }, getSVG: function (a) {
                var b, e, c, v, m, g = n(this.options, a); e = p("div", null, { position: "absolute", top: "-9999em", width: this.chartWidth + "px", height: this.chartHeight + "px" }, z.body); c = this.renderTo.style.width; m = this.renderTo.style.height; c = g.exporting.sourceWidth || g.chart.width || /px$/.test(c) && parseInt(c, 10) || 600; m = g.exporting.sourceHeight || g.chart.height || /px$/.test(m) &&
                parseInt(m, 10) || 400; r(g.chart, { animation: !1, renderTo: e, forExport: !0, renderer: "SVGRenderer", width: c, height: m }); g.exporting.enabled = !1; delete g.data; g.series = []; h(this.series, function (a) { v = n(a.userOptions, { animation: !1, enableMouseTracking: !1, showCheckbox: !1, visible: a.visible }); v.isInternal || g.series.push(v) }); h(this.axes, function (a) { a.userOptions.internalKey || (a.userOptions.internalKey = f.uniqueKey()) }); b = new f.Chart(g, this.callback); a && h(["xAxis", "yAxis", "series"], function (c) {
                    var d = {}; a[c] && (d[c] = a[c],
                    b.update(d))
                }); h(this.axes, function (a) { var c = f.find(b.axes, function (b) { return b.options.internalKey === a.userOptions.internalKey }), d = a.getExtremes(), e = d.userMin, d = d.userMax; !c || void 0 === e && void 0 === d || c.setExtremes(e, d, !0, !1) }); c = b.getChartHTML(); c = this.sanitizeSVG(c, g); g = null; b.destroy(); C(e); return c
            }, getSVGForExport: function (a, b) {
                var e = this.options.exporting; return this.getSVG(n({ chart: { borderRadius: 0 } }, e.chartOptions, b, {
                    exporting: {
                        sourceWidth: a && a.sourceWidth || e.sourceWidth, sourceHeight: a && a.sourceHeight ||
                        e.sourceHeight
                    }
                }))
            }, exportChart: function (a, b) { b = this.getSVGForExport(a, b); a = n(this.options.exporting, a); f.post(a.url, { filename: a.filename || "chart", type: a.type, width: a.width || 0, scale: a.scale, svg: b }, a.formAttributes) }, print: function () {
                var a = this, b = a.container, e = [], c = b.parentNode, f = z.body, m = f.childNodes, g = a.options.exporting.printMaxWidth, d, t; if (!a.isPrinting) {
                    a.isPrinting = !0; a.pointer.reset(null, 0); F(a, "beforePrint"); if (t = g && a.chartWidth > g) d = [a.options.chart.width, void 0, !1], a.setSize(g, void 0, !1);
                    h(m, function (a, b) { 1 === a.nodeType && (e[b] = a.style.display, a.style.display = "none") }); f.appendChild(b); E.focus(); E.print(); setTimeout(function () { c.appendChild(b); h(m, function (a, b) { 1 === a.nodeType && (a.style.display = e[b]) }); a.isPrinting = !1; t && a.setSize.apply(a, d); F(a, "afterPrint") }, 1E3)
                }
            }, contextMenu: function (a, b, e, c, v, m, g) {
                var d = this, t = d.options.navigation, k = d.chartWidth, q = d.chartHeight, n = "cache-" + a, l = d[n], x = Math.max(v, m), y, A; l || (d[n] = l = p("div", { className: a }, { position: "absolute", zIndex: 1E3, padding: x + "px" },
                d.container), y = p("div", { className: "highcharts-menu" }, null, l), u(y, r({ MozBoxShadow: "3px 3px 10px #888", WebkitBoxShadow: "3px 3px 10px #888", boxShadow: "3px 3px 10px #888" }, t.menuStyle)), A = function () { u(l, { display: "none" }); g && g.setState(0); d.openMenu = !1 }, d.exportEvents.push(w(l, "mouseleave", function () { l.hideTimer = setTimeout(A, 500) }), w(l, "mouseenter", function () { clearTimeout(l.hideTimer) }), w(z, "mouseup", function (b) { d.pointer.inClass(b.target, a) || A() })), h(b, function (a) {
                    "string" === typeof a && (a = d.options.exporting.menuItemDefinitions[a]);
                    if (f.isObject(a, !0)) { var b; a.separator ? b = p("hr", null, null, y) : (b = p("div", { className: "highcharts-menu-item", onclick: function (b) { b && b.stopPropagation(); A(); a.onclick && a.onclick.apply(d, arguments) }, innerHTML: a.text || d.options.lang[a.textKey] }, null, y), b.onmouseover = function () { u(this, t.menuItemHoverStyle) }, b.onmouseout = function () { u(this, t.menuItemStyle) }, u(b, r({ cursor: "pointer" }, t.menuItemStyle))); d.exportDivElements.push(b) }
                }), d.exportDivElements.push(y, l), d.exportMenuWidth = l.offsetWidth, d.exportMenuHeight =
                l.offsetHeight); b = { display: "block" }; e + d.exportMenuWidth > k ? b.right = k - e - v - x + "px" : b.left = e - x + "px"; c + m + d.exportMenuHeight > q && "top" !== g.alignOptions.verticalAlign ? b.bottom = q - c - x + "px" : b.top = c + m - x + "px"; u(l, b); d.openMenu = !0
            }, addButton: function (a) {
                var b = this, e = b.renderer, c = n(b.options.navigation.buttonOptions, a), f = c.onclick, m = c.menuItems, g, d, k = c.symbolSize || 12; b.btnCount || (b.btnCount = 0); b.exportDivElements || (b.exportDivElements = [], b.exportSVGElements = []); if (!1 !== c.enabled) {
                    var h = c.theme, q = h.states, p = q && q.hover,
                    q = q && q.select, l; delete h.states; f ? l = function (a) { a.stopPropagation(); f.call(b, a) } : m && (l = function () { b.contextMenu(d.menuClassName, m, d.translateX, d.translateY, d.width, d.height, d); d.setState(2) }); c.text && c.symbol ? h.paddingLeft = D(h.paddingLeft, 25) : c.text || r(h, { width: c.width, height: c.height, padding: 0 }); d = e.button(c.text, 0, 0, l, h, p, q).addClass(a.className).attr({ "stroke-linecap": "round", title: b.options.lang[c._titleKey], zIndex: 3 }); d.menuClassName = a.menuClassName || "highcharts-menu-" + b.btnCount++; c.symbol &&
                    (g = e.symbol(c.symbol, c.symbolX - k / 2, c.symbolY - k / 2, k, k).addClass("highcharts-button-symbol").attr({ zIndex: 1 }).add(d), g.attr({ stroke: c.symbolStroke, fill: c.symbolFill, "stroke-width": c.symbolStrokeWidth || 1 })); d.add().align(r(c, { width: d.width, x: D(c.x, b.buttonOffset) }), !0, "spacingBox"); b.buttonOffset += (d.width + c.buttonSpacing) * ("right" === c.align ? -1 : 1); b.exportSVGElements.push(d, g)
                }
            }, destroyExport: function (a) {
                var b = a ? a.target : this; a = b.exportSVGElements; var e = b.exportDivElements, c = b.exportEvents, f; a && (h(a,
                function (a, c) { a && (a.onclick = a.ontouchstart = null, f = "cache-" + a.menuClassName, b[f] && delete b[f], b.exportSVGElements[c] = a.destroy()) }), a.length = 0); e && (h(e, function (a, c) { clearTimeout(a.hideTimer); I(a, "mouseleave"); b.exportDivElements[c] = a.onmouseout = a.onmouseover = a.ontouchstart = a.onclick = null; C(a) }), e.length = 0); c && (h(c, function (a) { a() }), c.length = 0)
            }
        }); K.menu = function (a, b, e, c) { return ["M", a, b + 2.5, "L", a + e, b + 2.5, "M", a, b + c / 2 + .5, "L", a + e, b + c / 2 + .5, "M", a, b + c - 1.5, "L", a + e, b + c - 1.5] }; B.prototype.renderExporting = function () {
            var a =
            this, b = a.options.exporting, e = b.buttons, c = a.isDirtyExporting || !a.exportSVGElements; a.buttonOffset = 0; a.isDirtyExporting && a.destroyExport(); c && !1 !== b.enabled && (a.exportEvents = [], G(e, function (b) { a.addButton(b) }), a.isDirtyExporting = !1); w(a, "destroy", a.destroyExport)
        }; B.prototype.callbacks.push(function (a) { a.renderExporting(); w(a, "redraw", a.renderExporting); h(["exporting", "navigation"], function (b) { a[b] = { update: function (e, c) { a.isDirtyExporting = !0; n(!0, a.options[b], e); D(c, !0) && a.redraw() } } }) })
    })(k)
});