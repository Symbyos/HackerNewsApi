### How to run the application :

- Install `Docker`:

- On `Windows` https://www.docker.com/products/docker-desktop/ 
- On other OSes using the package manager shipped

In a `POSIX` (`Unix` like environment) terminal, type the following commands :

- `git clone https://github.com/Symbyos/HackerNewsApi.git`
- `cd HackerNewsApi/HackerNewsApi`
- `docker build ../. -f Dockerfile -t hackernewsapi --progress=plain`
- `docker run -d --name=hackernewsapi -p 8080:80 -p 8443:443 hackernewsapi`

In a `Windows` based terminal, type the following commands :

- `git clone https://github.com/Symbyos/HackerNewsApi.git`
- `cd HackerNewsApi\HackerNewsApi`
- `docker build ../. -f Dockerfile -t hackernewsapi --progress=plain`
- `docker run -d --name=hackernewsapi -p 8080:80 -p 8443:443 hackernewsapi`

Once the deployment is finished, you can query the following endpoint (please note that no prefetching is implemented, the first call to the API will be pretty slow as the service needs to initialize its cache) :

- `http://localhost:8080/hackernews`

For example with `cURL` :

- `curl http://localhost:8080/hackernews` or as a prettified output ` curl http://localhost:8080/hackernews | jq`

Output :

`[{"title":"What every software developer must know about Unicode in 2023","uri":"https://tonsky.me/blog/unicode/","postedBy":"mrzool","time":"2023-10-02T09:22:05","score":969,"descendants":546},{"title":"Return to Office Is Bullshit and Everyone Knows It","uri":"https://soatok.blog/2023/10/02/return-to-office-is-bullshit-and-everyone-knows-it/","postedBy":"Kye","time":"2023-10-02T15:28:10","score":906,"descendants":873},{"title":"An interactive intro to CRDTs","uri":"https://jakelazaroff.com/words/an-interactive-intro-to-crdts/","postedBy":"jakelazaroff","time":"2023-10-04T13:12:45...`

### Assumptions :

Based on the current HackerNews API description https://github.com/HackerNews/API#uri-and-versioning, there is no rate limit and it could change in the future. A possible rate limiter strategy could be implemented, if there is any not documented limit, as it happens with some data providers.

As commented [there](https://github.com/Symbyos/HackerNewsApi/blob/38029ea095e94265f81c5092cc16cf2aa6155470/HackerNewsApi/Services/HackerNewsApiService.cs#L77), the usage of a lock primitive could slow down parallel queries.
Thus, to preserve the performance of the application, I preferred to allow multiple insertion into the cache, in place of locking, as each news is never invalidated.
Also, if the application is never restarted, the cache could cause an OutOfMemoryException, as it will expand indefinitely and will not be collected by the Garbage Collector. But the current memory footprint is pretty low.

### Next steps :

- Add prefetching of the queries at application startup and in a background thread loop, to serve quickly new queries with the up-to-date cache
- Add certificate to the endpoint for secure connection
- Deploy the service in a `Kubernetes` cluster in a cloud provider of choice and add redundancy with multiple pods
- Add an authorization protocols (`OAuth`...) to filter out not authorized users
- Add efficient in-memory caching (`Redis`, ...) with persistency; or use a DBMS like `Micosoft SQL Server`, `PostgreSQL`, `MariaDB`, `MongoDB`...
- Add support for http, https, socks and tor proxies to spread out the query load to the external `HackerNews API`
- Add log handling with a succinct convention (write error codes, display context, ...)
- Externalize logs into a logging stack like `ELK` (`Elasticsearch`, `Logstash`, `Kibana`) or other solutions
- Add a load balancer to handle more queries to our API (`HAProxy`, ...)
- Add a retry strategy when not receiving status code 200 to the http queries
- Add unit tests and integration tests
- Add GitHub workflow to publish and test releases
