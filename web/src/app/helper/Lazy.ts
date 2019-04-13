export class Lazy<T> {

  private value: T = null;

  constructor(private callback: () => T | Promise<T>) {}

  public async GetAsync() {
    if (this.value === null){
      const result = this.callback();
      if (result.constructor.name === Promise.name) {
        this.value = (await result) as T;
      } else {
        this.value = result as T;
      }
    }

    return this.value;
  }

}
