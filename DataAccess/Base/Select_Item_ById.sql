select 
	Item.Id as item_id, 
	Item.Name as item_name,
	Category.code as category_code,
	Category.Name as category_name
from Item
inner join Category
on Item.CategoryCode = Category.Code
where Item.Id = @ItemId