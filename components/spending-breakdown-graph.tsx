"use client";

import ReactECharts from "echarts-for-react";

export default function SpendingBreakdownGraph() {
  const total = 3420;
  const data = [
    { value: 45, name: "Shopping", color: "#F97316" }, // orange
    { value: 30, name: "Food", color: "#FDE68A" }, // yellow
    { value: 25, name: "Transport", color: "#FEF3C7" }, // light yellow
  ];
  const option = {
    tooltip: {
      trigger: "item",
      formatter: "{b}: {d}%",
    },
    legend: {
      show: false, // hide ECharts legend
    },
    series: [
      {
        name: "Spending",
        type: "pie",
        radius: ["70%", "100%"], // donut
        avoidLabelOverlap: false,
        label: {
          show: true,
          position: "center",
          formatter: `$${total}\nTOTAL`,
          fontSize: 16,
          fontWeight: "bold",
          //color: "#374151", // gray-700
          lineHeight: 20,
        },
        labelLine: { show: false },
        data: data.map((item) => ({
          value: item.value,
          name: item.name,
          itemStyle: { color: item.color },
        })),
      },
    ],
  };

  return (
    <div className="bg-white dark:bg-gray-800 dark:border dark:border-gray-700 rounded-xl shadow p-5 w-full">
      <h2 className="text-sm font-bold text-gray-900 dark:text-white">
        Spending Breakdown
      </h2>
      <div className="flex">
        <div className="w-full h-36 rounded-lg flex items-start justify-start text-gray-500">
          <ReactECharts
            option={option}
            style={{ width: "80%", height: "100%" }}
          />
        </div>
        <div className="flex flex-col justify-center gap-2">
          {data.map((item) => (
            <div
              key={item.name}
              className="flex items-center justify-between w-40"
            >
              <div className="flex items-center gap-2">
                <span
                  className="w-3 h-3 rounded-full"
                  style={{ backgroundColor: item.color }}
                />
                <span className="text-xs font-semibold text-gray-700 dark:text-gray-300">
                  {item.name}
                </span>
              </div>
              <span className="text-xs font-bold text-gray-900 dark:text-white">
                {item.value}%
              </span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
