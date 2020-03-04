/************************************************************************/
/* Copyright (c) 2008 Cristian Adam.

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

    1. The origin of this software must not be misrepresented; you must not
    claim that you wrote the original software. If you use this software
    in a product, an acknowledgment in the product documentation would be
    appreciated but is not required.

    2. Altered source versions must be plainly marked as such, and must not be
    misrepresented as being the original software.

    3. This notice may not be removed or altered from any source
    distribution.

/************************************************************************/

#include <iostream>

// Disable any warnings issued by the import directives
#pragma warning (push, 0) 
#import <quartz.dll> rename_namespace("dshow")
#import <qedit.dll> rename_namespace("dshow")
#pragma warning (pop)

#include <strmif.h>
#include <vfwmsgs.h>
#include <uuids.h>

_COM_SMARTPTR_TYPEDEF(IFileSourceFilter, IID_IFileSourceFilter);
#pragma comment(lib, "strmiids.lib")

struct ComInitializer
{
    ComInitializer()
    {
        ::CoInitialize(0);
    }
    ~ComInitializer()
    {
        ::CoUninitialize();
    }
} g_comInit;

dshow::IPinPtr FindPin(const dshow::IBaseFilterPtr& filter, bool outputPin)
{
    using namespace dshow;
    
    IEnumPinsPtr enumPins;
    filter->EnumPins(&enumPins);

    IPinPtr pin;
    while (enumPins->Next(1, &pin, 0) == S_OK)
    {
        dshow::_PinInfo info;
        pin->QueryPinInfo(&info);

        IPinPtr connectedTo;
        HRESULT hr = pin->raw_ConnectedTo(&connectedTo);

        if (info.dir == (outputPin ? dshow::PINDIR_OUTPUT : dshow::PINDIR_INPUT) && 
            hr == VFW_E_NOT_CONNECTED)
        {
            return pin;
        }
        pin = 0;
    }

    return 0;
}

CLSID GetClsidFromString(const std::wstring& strGuid)
{
    CLSID clsid = {0};
    HRESULT hr = ::CLSIDFromString(const_cast<LPOLESTR>(strGuid.c_str()), &clsid);
    if (FAILED(hr)) throw _com_error(hr);

    return clsid;
}

void RenderFile(const dshow::IGraphBuilderPtr& graphBuilder, LPWSTR fileName)
{
    using namespace dshow;

    IBaseFilterPtr orbanParser;

    /* Orban Plugin CLSID taken from aacpParser.sxs.manifest 
     
     description="ORBAN-CT AAC/aacPlus Stream Parser"
     clsid="{301F7BDA-B1F8-4453-82B2-0B9187DF3F3F}"
    */

    HRESULT hr = orbanParser.CreateInstance(GetClsidFromString(L"{301F7BDA-B1F8-4453-82B2-0B9187DF3F3F}"));
    if (FAILED(hr)) throw _com_error(hr);

    graphBuilder->AddFilter(orbanParser, L"ORBAN-CT AAC/aacPlus Stream Parser");
    
    IFileSourceFilterPtr fileSource = orbanParser;
    fileSource->Load(fileName, 0);

    IPinPtr outputPin = FindPin(orbanParser, true);
    graphBuilder->Render(outputPin);
}

int wmain()
{
    using namespace dshow;
    using namespace std;

    try
    {
        IGraphBuilderPtr graphBuilder;

        HRESULT hr = graphBuilder.CreateInstance(CLSID_FilterGraph);
        if (FAILED(hr)) throw _com_error(hr);

        LPWSTR radioUrl = L"icyx://audio2.playradiouk.com:8020";
        RenderFile(graphBuilder, radioUrl);

        // For non registration free com you would have used
        // graphBuilder->RenderFile(radioUrl, 0);

        IMediaControlPtr control = graphBuilder;
        control->Run();

        wcout << L"Press Return to stop!" << endl;
        wcin.get();

        control->Stop();
    }
    catch(const _com_error& err)
    {
        wcout << err.ErrorMessage() << L". ErrorCode: 0x" << hex << err.Error() << endl;
    }
    
    return 0;
}
