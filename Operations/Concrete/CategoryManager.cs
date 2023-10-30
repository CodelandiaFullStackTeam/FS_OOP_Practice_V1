using FS_OOP_Practice_V1.Models;
using FS_OOP_Practice_V1.Operations.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS_OOP_Practice_V1.Operations.Concrete
{
    public class CategoryManager : ICategoryOperation
    {
        public IResult Add(Category item)
        {
            var duplicateNameCheck = CheckDuplicateName(item);
            if (!duplicateNameCheck.Success)
            {
                return duplicateNameCheck;
            }

            if (!(item.BaseCategory is not null && item.BaseCategory.IsActive == 1 && item.BaseCategory.Deleted == 0))
            {
                return new ErrorResult();
            }

            item.BaseCategory.ChildCount++;
            Update(item.BaseCategory);


            DB.Categories.Add(item);
            return new SuccessResult();
        }

        public IDataResult<Category> Get(int id)
        {
            return new SuccessDataResult<Category>(DB.Categories.FirstOrDefault(x => x.ID == id && x.Deleted == 0));
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(DB.Categories.Where(x => x.Deleted == 0).ToList());
        }

        public IDataResult<int> GetNextId()
        {
            if (!DB.Categories.Any())
            {
                return new SuccessDataResult<int>(1);
            }
            return new SuccessDataResult<int>(DB.Categories.Max(x => x.ID) + 1);
        }

        public IResult Remove(Category item)
        {
            //defensive coding
            // double check priority
            var deletedModel = DB.Categories.FirstOrDefault(x => x.ID == item.ID);
            deletedModel.Deleted = item.ID;

            if (deletedModel.BaseCategory is not null)
            {
                deletedModel.BaseCategory.ChildCount--;
                Update(deletedModel.BaseCategory);
            }

            Update(deletedModel);

            var subCategories = DB.Categories.Where(x => x.BaseCategory?.ID == item.ID).ToList();

            foreach (var subCategory in subCategories)
            {
                subCategory.Deleted = subCategory.ID;
                Update(subCategory);
            }

            return new SuccessResult();
        }

        public IResult Update(Category item)
        {
            var updatedItem = DB.Categories.FirstOrDefault(x => x.ID == item.ID);

            if (updatedItem.BaseCategory?.ID != item.BaseCategory?.ID)
            {
                updatedItem.BaseCategory.ChildCount--;
                item.BaseCategory.ChildCount++;
            }

            updatedItem = item;

            if (updatedItem.IsActive == 0)
            {
                var subCategories = DB.Categories.Where(x => x.BaseCategory?.ID == updatedItem.ID).ToList();
                foreach (var subCategory in subCategories)
                {
                    subCategory.IsActive = 0;
                    Update(subCategory);
                }
            }

            return new SuccessResult();
        }

        public IResult CheckDuplicateName(Category category)
        {
            var existingModel = DB.Categories.FirstOrDefault(x => x.Name == category.Name && x.Deleted == 0);
            if (existingModel is not null)
            {
                return new ErrorResult("Category is already exists");
            }
            return new SuccessResult();
        }
    }
}
