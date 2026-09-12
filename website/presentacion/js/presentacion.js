(function () {
  var slides = Array.prototype.slice.call(document.querySelectorAll(".slide"));
  var total = slides.length;
  var index = 0;
  var countEl = document.getElementById("count");
  var progressEl = document.getElementById("progress");
  var prevBtn = document.getElementById("prev");
  var nextBtn = document.getElementById("next");
  var printBtn = document.getElementById("print");
  var dotsEl = document.getElementById("dots");
  var body = document.body;

  function buildDots() {
    slides.forEach(function (_slide, i) {
      var li = document.createElement("li");
      var btn = document.createElement("button");
      btn.type = "button";
      btn.setAttribute("aria-label", "Ir a la diapositiva " + (i + 1));
      btn.addEventListener("click", function () {
        go(i);
      });
      li.appendChild(btn);
      dotsEl.appendChild(li);
    });
  }

  function go(next) {
    if (next < 0 || next >= total) {
      return;
    }
    slides[index].classList.remove("is-active");
    index = next;
    slides[index].classList.add("is-active");
    countEl.textContent = index + 1 + " / " + total;
    progressEl.style.width = ((index + 1) / total) * 100 + "%";
    prevBtn.disabled = index === 0;
    nextBtn.disabled = index === total - 1;
    body.classList.toggle("on-cover", slides[index].classList.contains("slide--cover"));
    Array.prototype.forEach.call(dotsEl.querySelectorAll("button"), function (btn, i) {
      btn.classList.toggle("is-active", i === index);
    });
  }

  function printDeck() {
    window.print();
  }

  prevBtn.addEventListener("click", function () {
    go(index - 1);
  });
  nextBtn.addEventListener("click", function () {
    go(index + 1);
  });
  printBtn.addEventListener("click", printDeck);

  document.addEventListener("keydown", function (event) {
    if (event.altKey || event.ctrlKey || event.metaKey) {
      return;
    }
    var key = event.key;
    if (key === "ArrowRight" || key === "ArrowDown" || key === "PageDown" || key === " ") {
      event.preventDefault();
      go(index + 1);
    } else if (key === "ArrowLeft" || key === "ArrowUp" || key === "PageUp") {
      event.preventDefault();
      go(index - 1);
    } else if (key === "Home") {
      event.preventDefault();
      go(0);
    } else if (key === "End") {
      event.preventDefault();
      go(total - 1);
    } else if (key === "p" || key === "P") {
      event.preventDefault();
      printDeck();
    }
  });

  buildDots();
  go(0);
})();
