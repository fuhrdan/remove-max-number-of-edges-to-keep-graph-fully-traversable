//*****************************************************************************
//** 1579. Remove Max Number of Edges to Keep Graph Fully Traversable        **
//** leetcode                                                                **
//*****************************************************************************
//*****************************************************************************


// Structure for UnionFind
typedef struct {
    int cnt;
    int *p;
    int *size;
} UnionFind;

// Function to initialize UnionFind
UnionFind* UnionFindCreate(int n) {
    UnionFind *uf = (UnionFind *)malloc(sizeof(UnionFind));
    uf->cnt = n;
    uf->p = (int *)malloc(n * sizeof(int));
    uf->size = (int *)malloc(n * sizeof(int));
    for (int i = 0; i < n; i++) {
        uf->p[i] = i;
        uf->size[i] = 1;
    }
    return uf;
}

// Function to find the root of an element
int find(UnionFind *uf, int x) {
    if (uf->p[x] != x) {
        uf->p[x] = find(uf, uf->p[x]);
    }
    return uf->p[x];
}

// Function to unite two elements
int unite(UnionFind *uf, int a, int b) {
    int pa = find(uf, a - 1);
    int pb = find(uf, b - 1);
    if (pa == pb) {
        return 0;
    }
    if (uf->size[pa] > uf->size[pb]) {
        uf->p[pb] = pa;
        uf->size[pa] += uf->size[pb];
    } else {
        uf->p[pa] = pb;
        uf->size[pb] += uf->size[pa];
    }
    uf->cnt--;
    return 1;
}

// Function to free the memory allocated for UnionFind
void UnionFindFree(UnionFind *uf) {
    free(uf->p);
    free(uf->size);
    free(uf);
}

// Function to find the maximum number of edges to remove
int maxNumEdgesToRemove(int n, int** edges, int edgesSize, int* edgesColSize) {
    UnionFind *ufa = UnionFindCreate(n);
    UnionFind *ufb = UnionFindCreate(n);
    int ans = 0;
    for (int i = 0; i < edgesSize; i++) {
        int t = edges[i][0], u = edges[i][1], v = edges[i][2];
        if (t == 3) {
            if (unite(ufa, u, v)) {
                unite(ufb, u, v);
            } else {
                ans++;
            }
        }
    }
    for (int i = 0; i < edgesSize; i++) {
        int t = edges[i][0], u = edges[i][1], v = edges[i][2];
        if (t == 1 && !unite(ufa, u, v)) {
            ans++;
        }
        if (t == 2 && !unite(ufb, u, v)) {
            ans++;
        }
    }
    int result = (ufa->cnt == 1 && ufb->cnt == 1) ? ans : -1;
    UnionFindFree(ufa);
    UnionFindFree(ufb);
    return result;
}
