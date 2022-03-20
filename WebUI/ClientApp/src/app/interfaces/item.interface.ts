import { ICategory } from "./category.interface";

export interface IItem {
  id?: number,
  name?: string,
  category?: ICategory;
}
