Select 
	Item.Id as item_id, 
	Item.Name as item_name,
	Category.code as category_code,
	Category.Name as category_name,
	Item.Author as item_author,
	Item.Description as item_description
From Item
Inner Join Category
On Item.CategoryCode = Category.Code
Where Item.Id = @ItemId