"use client";

import ReactECharts from "echarts-for-react";

const option = {
  xAxis: {
    type: "category",
    data: ["Mon", "Tue", "Wed", "Thu", "Fri"],
    axisLine: { show: true, lineStyle: { color: "#e5e7eb" } },
    axisLabel: { show: true, color: "#6b7280" },
    axisTick: { show: false },
  },
  yAxis: {
    type: "value",
    axisLine: { show: false },
    axisLabel: { show: false, color: "#6b7280" },
    splitLine: { show: false, lineStyle: { color: "#e5e7eb" } },
  },
  tooltip: {
    trigger: "axis",
    formatter: "{b}: ${c}",
    axisPointer: {
      type: "none",
    },
  },
  series: [
    {
      type: "bar",
      barCategoryGap: "3%",
      data: [120, 200, 150, 80, 70],
      itemStyle: {
        color: {
          type: "linear",
          x: 0,
          y: 0,
          x2: 0,
          y2: 1, // top to bottom
          colorStops: [
            { offset: 0.2, color: "#fb923c" }, // top
            { offset: 1, color: "#facc15" }, // bottom
          ],
        },
      },
    },
  ],
};

export function CategoryExpenseGraph() {
  return (
    <ReactECharts option={option} style={{ height: "100%", width: "100%" }} />
  );
}
