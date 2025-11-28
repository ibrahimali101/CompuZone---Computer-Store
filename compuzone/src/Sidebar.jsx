import "./Sidebar.css";
import { FaStar } from "react-icons/fa";

function Sidebar() {
  return (
    <aside className="sidebar">
      {/* 1. فلتر الفئات */}
      <div className="filter-group">
        <h4>الفئات</h4>
        <ul className="filter-list">
          <li>
            <input type="checkbox" id="cat-hardware" />
            <label htmlFor="cat-hardware">قطع هاردوير</label>
          </li>
          <li>
            <input type="checkbox" id="cat-builds" />
            <label htmlFor="cat-builds">تجميعات الكمبيوتر</label>
          </li>
          <li>
            <input type="checkbox" id="cat-laptops" />
            <label htmlFor="cat-laptops">اللاب توب</label>
          </li>
          <li>
            <input type="checkbox" id="cat-consoles" />
            <label htmlFor="cat-consoles">الكونسول</label>
          </li>
          <li>
            <input type="checkbox" id="cat-accessories" />
            <label htmlFor="cat-accessories">الاكسسوارات</label>
          </li>
          <li>
            <input type="checkbox" id="cat-monitors" />
            <label htmlFor="cat-monitors">الشاشات</label>
          </li>
        </ul>
      </div>

      {/* 2. فلتر السعر */}
      <div className="filter-group">
        <h4>السعر</h4>
        <div className="price-filter">
          <input type="number" placeholder="من" />
          <span>-</span>
          <input type="number" placeholder="إلى" />
          <button>تطبيق</button>
        </div>
      </div>

      {/* 3. فلتر الماركات (وهمي) */}
      <div className="filter-group">
        <h4>الماركات</h4>
        <ul className="filter-list">
          <li>
            <input type="checkbox" id="brand-intel" />
            <label htmlFor="brand-intel">Intel</label>
          </li>
          <li>
            <input type="checkbox" id="brand-amd" />
            <label htmlFor="brand-amd">AMD</label>
          </li>
          <li>
            <input type="checkbox" id="brand-nvidia" />
            <label htmlFor="brand-nvidia">Nvidia</label>
          </li>
          <li>
            <input type="checkbox" id="brand-samsung" />
            <label htmlFor="brand-samsung">Samsung</label>
          </li>
        </ul>
      </div>

      {/* 4. فلتر التقييم (وهمي) */}
      <div className="filter-group">
        <h4>التقييم</h4>
        <ul className="filter-list rating-filter">
          <li>
            <input type="radio" name="rating" id="rate-4" />
            <label htmlFor="rate-4">
              <FaStar /> <FaStar /> <FaStar /> <FaStar /> <span>وأكثر</span>
            </label>
          </li>
          <li>
            <input type="radio" name="rating" id="rate-3" />
            <label htmlFor="rate-3">
              <FaStar /> <FaStar /> <FaStar /> <span>وأكثر</span>
            </label>
          </li>
        </ul>
      </div>
    </aside>
  );
}

export default Sidebar;
