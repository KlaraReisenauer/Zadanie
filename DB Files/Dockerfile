FROM mcr.microsoft.com/mssql/server:2019-latest AS build
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=SQLDB.CONTAINER
WORKDIR /tmp
COPY Employee_DB.bak .
COPY restore-backup.sql .
RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 20 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "SQLDB.CONTAINER" -i /tmp/restore-backup.sql \
    && pkill sqlservr

FROM mcr.microsoft.com/mssql/server:2019-latest AS release
ENV ACCEPT_EULA=Y
COPY --from=build /var/opt/mssql/data /var/opt/mssql/data