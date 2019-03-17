import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

enum HttpVerb {
  Get = 'get',
  Post = 'post',
  Put = 'put',
  Delete = 'delete'
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private static Prefix = '/api';

  constructor(
    private client: HttpClient
  ) { }

  private static getNormalizedPath(path: Array<any> | string): string {
    if (typeof(path) !== typeof('')) {
      // @ts-ignore: Callback signature mismatch
      path = (path as Array<any>).map(String.prototype.constructor).join('/');
    }
    return `${ApiService.Prefix}/${path}`;
  }

  private verb<T>(verb: string, path: Array<any> | string, model?: object, options: object = {}): Promise<T> {
    const normalizedPath = ApiService.getNormalizedPath(path);
    if (model) {
      if (verb === HttpVerb.Delete) {
        const deleteOptions = {
          headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
          body: model
        };
        options = { ...options, ...deleteOptions };
        return this.client
          .request(HttpVerb.Delete, normalizedPath, options)
          .toPromise() as Promise<T>;
      } else {
        return this.client[verb](normalizedPath, model, options).toPromise() as Promise<T>;
      }
    } else {
      return this.client[verb](normalizedPath, options).toPromise() as Promise<T>;
    }
  }

  get<T>(path: Array<any> | string, options?: object): Promise<T> {
    return this.verb<T>(HttpVerb.Get, path, undefined, options);
  }

  post<T>(path: Array<any> | string, model: object, options?: object): Promise<T> {
    return this.verb<T>(HttpVerb.Post, path, model, options);
  }

  delete(path: Array<any> | string, model?: object, options?: object): Promise<any> {
    return this.verb<any>(HttpVerb.Delete, path, model, options);
  }

  update<T>(path: Array<any> | string, model: object, options?: object): Promise<T> {
    return this.verb<T>(HttpVerb.Put, path, model, options);
  }

}
