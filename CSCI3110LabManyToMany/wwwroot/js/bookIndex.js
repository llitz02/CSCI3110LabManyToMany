// ES module for Book Index page
export function main() {
    console.log('bookIndex.js loaded');

    const assignAuthorModalDOM = document.getElementById('assignAuthorModal');
    const assignAuthorModal = assignAuthorModalDOM ? new bootstrap.Modal(assignAuthorModalDOM) : null;

    const btnClose = document.getElementById('btnAssignAuthorClose');
    if (btnClose && assignAuthorModal) {
        btnClose.addEventListener('click', (e) => {
            e.preventDefault();
            assignAuthorModal.hide();
        });
    }

    async function getAllAuthors() {
        try {
            const resp = await fetch('/author/getall');
            if (!resp.ok) throw new Error(`HTTP ${resp.status}`);
            const authors = await resp.json();
            return authors;
        } catch (err) {
            console.error('Error fetching authors:', err);
            return [];
        }
    }

    async function assignAuthorToBook(bookId, authorId) {
        try {
            const url = `/book/assignauthor/${encodeURIComponent(bookId)}?authorId=${encodeURIComponent(authorId)}`;
            const resp = await fetch(url, { method: 'POST' });
            if (!resp.ok) {
                const txt = await resp.text();
                return { success: false, message: txt };
            }
            const json = await resp.json();
            return json;
        } catch (err) {
            console.error('Error assigning author:', err);
            return { success: false, message: err.message };
        }
    }

    function clearChildren(el) {
        while (el.firstChild) el.removeChild(el.firstChild);
    }

    function populateAuthors(authors, bookId) {
        const tbody = document.getElementById('authorTableBody');
        if (!tbody) return;
        clearChildren(tbody);

        authors.forEach(author => {
            const row = document.createElement('tr');

            const tdId = document.createElement('td');
            tdId.textContent = author.id ?? author.Id ?? '';
            row.appendChild(tdId);

            const tdName = document.createElement('td');
            const fullName = `${author.firstName ?? author.FirstName ?? ''} ${author.lastName ?? author.LastName ?? ''}`.trim();
            tdName.textContent = fullName || '(No name)';
            row.appendChild(tdName);

            const tdBtn = document.createElement('td');
            const btn = document.createElement('button');
            btn.className = 'assignAuthorBtn btn btn-sm btn-primary';
            btn.textContent = 'Assign';
            btn.setAttribute('data-book-id', bookId);
            btn.setAttribute('data-author-id', author.id ?? author.Id ?? '');
            tdBtn.appendChild(btn);
            row.appendChild(tdBtn);

            tbody.appendChild(row);
        });

        // attach listeners to assign buttons
        document.querySelectorAll('.assignAuthorBtn').forEach(btn => {
            btn.addEventListener('click', async (e) => {
                e.preventDefault();
                const target = e.currentTarget;
                const bId = target.getAttribute('data-book-id');
                const aId = target.getAttribute('data-author-id');
                const result = await assignAuthorToBook(bId, aId);
                if (result && (result === 'Ok' || result.ok || result.success)) {
                    alert('Author assigned successfully!');
                } else {
                    alert('Author assignment failed!');
                }
                if (assignAuthorModal) assignAuthorModal.hide();
                window.location.reload();
            });
        });
    }

    function setupLinks() {
        document.querySelectorAll('a.btn-warning').forEach(link => {
            link.addEventListener('click', async (e) => {
                e.preventDefault();
                const href = link.getAttribute('href');
                console.log('Assign link clicked:', href);
                // extract book id from href; expected pattern /book/assignauthor/{id}
                const parts = href ? href.split('/') : [];
                const bookId = parts.length ? parts[parts.length - 1] : '';

                if (assignAuthorModal) assignAuthorModal.show();

                try {
                    const authors = await getAllAuthors();
                    console.log('Authors fetched:', authors);
                    populateAuthors(authors, bookId);
                } catch (err) {
                    console.error('Error in click handler:', err);
                }
            });
        });
    }

    // initialize
    setupLinks();
}

// Auto-run main when module is loaded
document.addEventListener('DOMContentLoaded', () => {
    main();
});
