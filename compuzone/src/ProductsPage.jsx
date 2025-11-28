import { useState } from "react"; // <-- 1. استدعي useState
import "./ProductsPage.css";
import Sidebar from "./Sidebar";
import ProductGrid from "./ProductGrid";
import Pagination from "./Pagination"; // <-- 2. استدعي الترقيم

// 3. الداتا الوهمية الكاملة (هنخليها 12 منتج عشان يبقوا صفحتين)
const allProducts = [
  {
    id: 1,
    imgSrc: "https://cdn.dummyjson.com/product-images/6/1.webp",
    title: "لابتوب جيمنج G15",
    oldPrice: "35000",
    newPrice: "32500",
    discountPercentage: 7,
  },
  {
    id: 2,
    imgSrc: "https://cdn.dummyjson.com/product-images/93/1.webp",
    title: "شاشة سامسونج 27 بوصة",
    oldPrice: "8000",
    newPrice: "7100",
    discountPercentage: 11,
  },
  {
    id: 3,
    imgSrc: "https://cdn.dummyjson.com/product-images/95/1.webp",
    title: "كارت شاشة ZOTAC RTX 4070",
    oldPrice: "25000",
    newPrice: "23000",
    discountPercentage: 8,
  },
  {
    id: 4,
    imgSrc: "https://cdn.dummyjson.com/product-images/96/1.webp",
    title: "ماوس لوجيتك G Pro",
    oldPrice: "4000",
    newPrice: "3200",
    discountPercentage: 20,
  },
  {
    id: 5,
    imgSrc: "https://cdn.dummyjson.com/product-images/97/1.webp",
    title: "كيبورد ميكانيكال RGB",
    oldPrice: "2200",
    newPrice: "1800",
    discountPercentage: 18,
  },
  {
    id: 6,
    imgSrc: "https://cdn.dummyjson.com/product-images/98/1.webp",
    title: "سماعة جيمنج HyperX",
    oldPrice: "3000",
    newPrice: "2650",
    discountPercentage: 12,
  },
  {
    id: 7,
    imgSrc: "https://cdn.dummyjson.com/product-images/90/1.webp",
    title: "باور سبلاي 750W Gold",
    oldPrice: "4500",
    newPrice: "4000",
    discountPercentage: 11,
  },
  {
    id: 8,
    imgSrc: "https://cdn.dummyjson.com/product-images/86/1.webp",
    title: "هارد M.2 SSD 1TB",
    oldPrice: "5000",
    newPrice: "4300",
    discountPercentage: 14,
  },
  {
    id: 9,
    imgSrc: "https://cdn.dummyjson.com/product-images/1/1.webp",
    title: "iPhone 9",
    oldPrice: "15000",
    newPrice: "14200",
    discountPercentage: 5,
  },
  {
    id: 10,
    imgSrc: "https://cdn.dummyjson.com/product-images/12/1.webp",
    title: "موبايل سامسونج S22",
    oldPrice: "30000",
    newPrice: "28000",
    discountPercentage: 7,
  },
  {
    id: 11,
    imgSrc: "https://cdn.dummyjson.com/product-images/2/1.webp",
    title: "iPhone X",
    oldPrice: "22000",
    newPrice: "20500",
    discountPercentage: 8,
  },
  {
    id: 12,
    imgSrc: "https://cdn.dummyjson.com/product-images/3/1.webp",
    title: "Samsung Universe 9",
    oldPrice: "9000",
    newPrice: "8100",
    discountPercentage: 10,
  },
];

const ITEMS_PER_PAGE = 6; // <-- 4. حدد العدد اللي عايزه في الصفحة

function ProductsPage() {
  // 5. حالة عشان نعرف إحنا في أنهي صفحة
  const [currentPage, setCurrentPage] = useState(1);

  // 6. الحسابات السحرية
  const indexOfLastItem = currentPage * ITEMS_PER_PAGE;
  const indexOfFirstItem = indexOfLastItem - ITEMS_PER_PAGE;
  // 7. دي المنتجات اللي هتتعرض (الـ 6 بتوع الصفحة دي)
  const currentProducts = allProducts.slice(indexOfFirstItem, indexOfLastItem);

  return (
    <div className="products-page-container">
      <div className="products-content">
        {/* 8. ابعت الـ 6 منتجات بس للج్రిد */}
        <ProductGrid products={currentProducts} />

        {/* 9. ضيف أزرار الترقيم تحت الجريد */}
        <Pagination
          itemsPerPage={ITEMS_PER_PAGE}
          totalItems={allProducts.length}
          currentPage={currentPage}
          onPageChange={setCurrentPage}
        />
      </div>
      <div className="sidebar-container">
        <Sidebar />
      </div>
    </div>
  );
}

export default ProductsPage;
