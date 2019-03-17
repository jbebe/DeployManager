import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ServerInfo } from '../entity/ServerInfo';
import { ResourceTypeInfo, ResourceTypeQuery } from '../entity/ResourceType';

@Injectable({
  providedIn: 'root'
})
export class InfoService {

  private resourceTypes: ResourceTypeQuery = null;

  public serverInfo: ServerInfo[] = [];
  public serverTypes: ResourceTypeInfo[] = [];
  public deployTypes: ResourceTypeInfo[] = [];

  constructor(
    private api: ApiService
  ) {
    this.initInfoAsync();
  }

  private async initInfoAsync() {
    this.serverInfo = await this.api.get<ServerInfo[]>('serverinfo');
    this.resourceTypes = await this.api.get<ResourceTypeQuery>('resourcetype');

    this.serverTypes = this.resourceTypes.serverTypes;
    this.deployTypes = this.resourceTypes.deployTypes;
  }

  public async getServerInstancesAsync(): Promise<ServerInfo[]> {
    if (this.serverInfo === null) {
      await this.initInfoAsync();
    }
    return this.serverInfo;
  }

  public async getServerTypesAsync(): Promise<ResourceTypeInfo[]> {
    if (this.resourceTypes === null) {
      await this.initInfoAsync();
    }
    return this.resourceTypes.serverTypes;
  }

  public async getDeployTypesAsync(): Promise<ResourceTypeInfo[]> {
    if (this.resourceTypes === null) {
      await this.initInfoAsync();
    }
    return this.resourceTypes.deployTypes;
  }
}
