export class Sale {
  constructor(clientId = "", branch = "", items = []) {
    this.clientId = clientId;
    this.branch = branch;
    this.items = items;
  }

  static createEmpty() {
    return new Sale("", "", []);
  }

  addItem(item) {
    this.items.push(item);
  }

  removeItem(index) {
    this.items.splice(index, 1);
  }

  updateItem(index, field, value) {
    this.items[index] = { ...this.items[index], [field]: value };
  }
}
