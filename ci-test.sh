#!/bin/bash

(cd sample/SampleApp; dotnet run) & (cd test/BrowserInterop.E2ETests; npm run-script run)