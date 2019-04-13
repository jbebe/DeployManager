import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ResourceTypeInfo, ResourceTypeQuery } from '../entity/ResourceType';
import { Lazy } from '../helper/Lazy';

@Injectable({
  providedIn: 'root'
})
export class InfoService {

  private resourceTypes: Lazy<ResourceTypeQuery> =
    new Lazy<ResourceTypeQuery>(() => this.api.get<ResourceTypeQuery>('resourcetype'));

  public serverTypes: Lazy<ResourceTypeInfo[]> =
    new Lazy<ResourceTypeInfo[]>(async () => {
      return (await this.resourceTypes.GetAsync()).serverTypes;
    });
  public deployTypes: Lazy<ResourceTypeInfo[]> =
    new Lazy<ResourceTypeInfo[]>(async () => {
      return (await this.resourceTypes.GetAsync()).deployTypes;
    });

  constructor(
    private api: ApiService
  ) {}

  public async getServerTypesAsync(): Promise<ResourceTypeInfo[]> {
    return this.serverTypes.GetAsync();
  }

  public async getDeployTypesAsync(): Promise<ResourceTypeInfo[]> {
    return this.deployTypes.GetAsync();
  }
}
