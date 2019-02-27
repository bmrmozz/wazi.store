FROM microsoft/dotnet:1.0.0-preview2-sdk

WORKDIR /app

ADD src/wazi.store src/wazi.store
ADD wazi.store.common wazi.store.common
ADD wazi.store.models wazi.store.models

RUN dotnet restore -v minimal src/ \
    && dotnet publish -c Release -o ./ src/wazi.store/ \
    && rm -rf src/ $HOME/.nuget/