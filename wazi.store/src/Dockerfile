FROM microsoft/aspnetcore;

WORKDIR /app

ADD src/wazi.store src/wazi.store
ADD wazi.store.common wazi.store.common
ADD wazi.store.models wazi.store.models

RUN dotnet restore -v minimal src/ \
    && dotnet publish -c Release -o ./ src/wazi.store/ \
    && rm -rf src/ $HOME/.nuget/