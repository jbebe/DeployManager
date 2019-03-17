export class ResourceTypeInfo {

  public id: number;
  public name: string;
}

export class ResourceTypeQuery {

  public deployTypes: ResourceTypeInfo[];
  public serverTypes: ResourceTypeInfo[];
}
