CREATE PROC [dbo].[cust_rock_sp_delete_attribute_valuesByAttributeId]
@AttributeId int

AS

DECLARE @RockAttributeId int

SELECT
	 @RockAttributeId = [foreign_key]
FROM [core_attribute] WITH (NOLOCK)
WHERE [attribute_id] = @AttributeId

IF @RockAttributeId IS NOT NULL
BEGIN

	DELETE [RockChMS].[dbo].[coreAttributeValue] 
	WHERE [AttributeId] = @AttributeId
	
END


