namespace InkRendererDemo
{
    using DirectN;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
    using System.Runtime.InteropServices;
    using Windows.UI.Input.Inking;
    using WinRT;

    [TestClass]
    public class UnitTest1
    {
        [UITestMethod]
        public void RenderInkStrokes()
        {
            using var d3D11Device = D3D11Functions.D3D11CreateDevice(null, D3D_DRIVER_TYPE.D3D_DRIVER_TYPE_HARDWARE, D3D11_CREATE_DEVICE_FLAG.D3D11_CREATE_DEVICE_BGRA_SUPPORT);
            using var dxgiDevice = d3D11Device.AsComObject<IDXGIDevice>();

            using var d2D1Factory1 = D2D1Functions.D2D1CreateFactory1(D2D1_FACTORY_TYPE.D2D1_FACTORY_TYPE_SINGLE_THREADED);

            using var d2D1Device = d2D1Factory1.CreateDevice(dxgiDevice);
            using var d2D1DeviceContext = d2D1Device.CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS.D2D1_DEVICE_CONTEXT_OPTIONS_NONE);

            var rendererObj = new InkD2DRenderer();
            var renderer = rendererObj.As<IInkD2DRenderer>();

            var inkStrokeContainer = new InkStrokeContainer();
            var strokes = inkStrokeContainer.GetStrokes();

            renderer.Draw(d2D1DeviceContext.Object, strokes, false).ThrowOnError();
        }
    }

    [ComImport]
    [Guid("4044e60c-7b01-4671-a97c-04e0210a07a5")]
    public class InkD2DRenderer
    {
    }
}